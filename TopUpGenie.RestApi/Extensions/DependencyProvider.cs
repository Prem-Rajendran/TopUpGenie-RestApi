﻿namespace TopUpGenie.RestApi.Extensions;

/// <summary>
/// DependencyProvider
/// </summary>
public static class DependencyProvider
{
    /// <summary>
    /// AddDependencies
    /// </summary>
    /// <param name="builder"></param>
	public static void AddDependencies(this WebApplicationBuilder builder)
	{
        // JWT Token Services
        builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("Jwt"));

        var jwtSettings = builder.Configuration.GetSection("Jwt").Get<JwtSettings>();
        var key = Encoding.ASCII.GetBytes(jwtSettings?.Key ?? "");

        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.RequireHttpsMetadata = false;  // Set to true in production
            options.SaveToken = true;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtSettings?.Issuer,
                ValidAudience = jwtSettings?.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ClockSkew = TimeSpan.Zero  // Adjust the time skew tolerance
            };
        });
    }

    /// <summary>
    /// AddExternalDependency
    /// </summary>
    /// <param name="builder"></param>
    public static void AddExternalDependency(this WebApplicationBuilder builder)
    {
        bool useMyExternalService = builder.Configuration.GetSection("UseMyExternalService").Get<bool>();

        if (useMyExternalService)
            builder.Services.AddScoped<IExternalService, MyExternalService>();
        else
            builder.Services.AddScoped<IExternalService, ExternalService>();
    }

    /// <summary>
    /// AddRepositories
    /// </summary>
    /// <param name="builder"></param>
    public static void AddRepositories(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddScoped<IBeneficiaryRepository, BenificiaryRepository>();
        builder.Services.AddScoped<ISessionRepository, SessionRepository>();
        builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();
        builder.Services.AddScoped<ITopUpOptionsRepository, TopUpOptionsRepository>();
        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
        builder.Services.AddScoped<ITransactionUnitOfWork, TransactionUnitOfWork>();
    }

    /// <summary>
    /// AddServices
    /// </summary>
    /// <param name="builder"></param>
    public static void AddServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IAdminService, AdminService>();
        builder.Services.AddScoped<IAuthService, AuthService>();
        builder.Services.AddScoped<ITokenService, TokenService>();
        builder.Services.AddScoped<IProfileService, ProfileService>();
        builder.Services.AddScoped<IBeneficiaryService, BeneficiaryService>();
        builder.Services.AddScoped<ITopUpService, TopUpService>();
        builder.Services.AddScoped<ITransactionService, TransactionService>();
    }

    /// <summary>
    /// AddDbContext
    /// </summary>
    /// <param name="builder"></param>
    public static void AddDbContext(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<TopUpGenieDbContext>();
        builder.Services.AddDbContext<TransactionDbContext>();
    }

}

