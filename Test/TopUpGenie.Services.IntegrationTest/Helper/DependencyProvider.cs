using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.DataCollection;
using Moq;
using TopUpGenie.Common;
using TopUpGenie.Services.IntegrationTest.Seeder;

namespace TopUpGenie.Services.IntegrationTest.Helper;

public static class DependencyProvider
{
    private static readonly Lazy<IServiceProvider> _serviceProvider = new Lazy<IServiceProvider>(InitializeServiceProvider);

    public static IServiceProvider ServiceProvider => _serviceProvider.Value;

    private static IServiceProvider InitializeServiceProvider()
    {
        var services = new ServiceCollection();

        // Services
        services.AddScoped<IAdminService, AdminService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IProfileService, ProfileService>();
        services.AddScoped<IBeneficiaryService, BeneficiaryService>();
        services.AddScoped<ITopUpService, TopUpService>();
        services.AddScoped<ITransactionService, TransactionService>();
        services.AddScoped<IExternalService, MyExternalService>();

        // Repository
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IBeneficiaryRepository, BenificiaryRepository>();
        services.AddScoped<ISessionRepository, SessionRepository>();
        services.AddScoped<ITransactionRepository, TransactionRepository>();
        services.AddScoped<ITopUpOptionsRepository, TopUpOptionsRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<ITransactionUnitOfWork, TransactionUnitOfWork>();

        // Settings
        services.Configure<JwtSettings>(options =>
        {
            options.Key = "A1b2C3d4E5f6G7h8I9j0K1l2M3n4O5p6Q7r8S9t0U1v2W3x4Y5z6A7b8C9d0E1f2";
            options.Issuer = "TopUpGenieIssuer";
            options.Audience = "TopUpGenieAudience";
            options.ExpireMinutes = 1;
        });

        // Database
        services.AddScoped<TopUpGenieDbContextFixture>();
        services.AddScoped<TransactionDbContextFixture>();

        services.AddScoped<TopUpGenieDbContext>(serviceProvider =>
        {
            TopUpGenieDbContextFixture dependency = serviceProvider.GetRequiredService<TopUpGenieDbContextFixture>();
            return dependency.DbContext;
        });

        services.AddScoped<TransactionDbContext>(serviceProvider =>
        {
            TransactionDbContextFixture dependency = serviceProvider.GetRequiredService<TransactionDbContextFixture>();
            return dependency.DbContext;
        });

        // Logging
        services.AddLogging(builder =>
        {
            builder.AddConsole();
            builder.AddDebug();
        });

        // Request Context
        var httpContextAccessor = new Mock<IHttpContextAccessor>();
        var httpContext = new DefaultHttpContext();
        httpContextAccessor.Setup(provider => provider.HttpContext).Returns(httpContext);
        services.AddSingleton<IHttpContextAccessor>(httpContextAccessor.Object);

        services.AddScoped(provider =>
        {
            var httpContextAccessor = provider.GetRequiredService<IHttpContextAccessor>();
            return new RequestContext(httpContextAccessor.HttpContext);
        });

        // Register any other dependencies your services need
        // e.g., logging, configuration, database context, etc.

        return services.BuildServiceProvider();
    }

    private static T GetService<T>()
    {
        return ServiceProvider.GetRequiredService<T>();
    }

    public static IAdminService AdminService => GetService<IAdminService>();
    public static IBeneficiaryService BeneficiaryService => GetService<IBeneficiaryService>();
    public static IAuthService AuthService => GetService<IAuthService>();
    public static IProfileService ProfileService => GetService<IProfileService>();
    public static ITokenService TokenService => GetService<ITokenService>();
    public static ITopUpService TopUpService => GetService<ITopUpService>();
    public static RequestContext RequestContext => GetService<RequestContext>();
    public static TransactionDbContext TransactionDbContext => GetService<TransactionDbContext>();

    #region TopUpGenie User
    public static void SeedTopupGenieUsers()
    {
        TopUpGenieDbContext topUpGenieDbContext = GetService<TopUpGenieDbContext>();
        var users = topUpGenieDbContext.Users.ToList();
        if (!users.Any())
        {
            topUpGenieDbContext.Users.AddRange(UserSeeding.GetUsers());
            topUpGenieDbContext.SaveChanges();
        }
    }

    public static void UnSeedTopupGenieUsers()
    {
        TopUpGenieDbContext topUpGenieDbContext = GetService<TopUpGenieDbContext>();
        var users = topUpGenieDbContext.Users.ToList();
        if (users.Any())
        {
            topUpGenieDbContext.Users.RemoveRange(topUpGenieDbContext.Users.ToList());
            topUpGenieDbContext.SaveChanges();
        }
    }
    #endregion

    #region TopUpGenie LoginSession
    public static List<string> SeedTopupGenieLoginSession()
    {
        SeedTopupGenieUsers();
        TopUpGenieDbContext topUpGenieDbContext = GetService<TopUpGenieDbContext>();
        var loginSessions = topUpGenieDbContext.LoginSessions.ToList();
        if (!loginSessions.Any())
        {
            var sessions = LoginSessionSeeding.GetLoginSessions();
            topUpGenieDbContext.LoginSessions.AddRange(LoginSessionSeeding.GetLoginSessions());
            topUpGenieDbContext.SaveChanges();
            return sessions.Select(s => s.AccessToken).ToList<string>();
        }

        return loginSessions.Select(s => s.AccessToken).ToList<string>();
    }

    public static void UnSeedTopupGenieLoginSession()
    {
        TopUpGenieDbContext topUpGenieDbContext = GetService<TopUpGenieDbContext>();
        var loginSessions = topUpGenieDbContext.LoginSessions.ToList();
        if (loginSessions.Any())
        {
            topUpGenieDbContext.LoginSessions.RemoveRange(loginSessions);
            topUpGenieDbContext.SaveChanges();
        }
    }
    #endregion

    #region Transaction User
    public static void SeedTransactionUser()
    {
        
        TransactionDbContext transactionDbContext = GetService<TransactionDbContext>();
        if (!transactionDbContext.Users.Any())
        {
            transactionDbContext.Users.AddRange(UserSeeding.GetUsers());
            transactionDbContext.SaveChanges();
        }
    }

    public static void UnSeedTransactionUser()
    {
        TransactionDbContext transactionDbContext = GetService<TransactionDbContext>();
        if (transactionDbContext.Users.Any())
        {
            transactionDbContext.Users.RemoveRange(transactionDbContext.Users.ToList());
            transactionDbContext.SaveChanges();
        }
    }
    #endregion

    #region TopupGenie Beneficiary

    public static void SeedTopupGenieBeneficiary()
    {
        SeedTopupGenieUsers();
        TopUpGenieDbContext topUpGenieDbContext = GetService<TopUpGenieDbContext>();
        var beneficiaries = topUpGenieDbContext.Beneficiaries.ToList();
        if (!beneficiaries.Any())
        {
            topUpGenieDbContext.Beneficiaries.AddRange(BeneficiarySeeding.GetBeneficiaries());
            topUpGenieDbContext.SaveChanges();
        }
    }

    public static void UnSeedTopupGenieBeneficiary()
    {
        TopUpGenieDbContext topUpGenieDbContext = GetService<TopUpGenieDbContext>();
        var beneficiaries = topUpGenieDbContext.Beneficiaries.ToList();
        if (beneficiaries.Any())
        {
            topUpGenieDbContext.Beneficiaries.RemoveRange(topUpGenieDbContext.Beneficiaries.ToList());
            topUpGenieDbContext.SaveChanges();
        }
    }

    #endregion

    #region Transaction Beneficiary
    public static void SeedTransactionBeneficiary()
    {
        SeedTransactionUser();
        
        TransactionDbContext transactionDbContext = GetService<TransactionDbContext>();

        if (!transactionDbContext.Beneficiaries.Any())
        {
            transactionDbContext.Beneficiaries.AddRange(BeneficiarySeeding.GetBeneficiaries());
            transactionDbContext.SaveChanges();
        }
    }

    public static void UnSeedTransactionBeneficiary()
    {
        TransactionDbContext transactionDbContext = GetService<TransactionDbContext>();

        if (transactionDbContext.Users.Any())
        {
            transactionDbContext.Beneficiaries.RemoveRange(transactionDbContext.Beneficiaries.ToList());
            transactionDbContext.SaveChanges();
        }
    }
    #endregion

    #region TopupGenie TopupOptions
    public static void SeedTopupGenieTopUpOptions()
    {
        TopUpGenieDbContext topUpGenieDbContext = GetService<TopUpGenieDbContext>();
        if (!topUpGenieDbContext.TopUpOptions.Any())
        {
            topUpGenieDbContext.TopUpOptions.AddRange(TopUpOptionsSeeding.GetTopUpOptions());
            topUpGenieDbContext.SaveChanges();
        }
    }

    public static void UnSeedTopupGenieTopUpOptions()
    {
        TopUpGenieDbContext topUpGenieDbContext = GetService<TopUpGenieDbContext>();
        if (topUpGenieDbContext.TopUpOptions.Any())
        {
            topUpGenieDbContext.TopUpOptions.RemoveRange(topUpGenieDbContext.TopUpOptions.ToList());
            topUpGenieDbContext.SaveChanges();
        }
    }
    #endregion

    #region Transaction TopupOptions
    public static void SeedTransactionTopUpOptions()
    {
        TransactionDbContext transactionDbContext = GetService<TransactionDbContext>();
        if (!transactionDbContext.TopUpOptions.Any())
        {
            transactionDbContext.TopUpOptions.AddRange(TopUpOptionsSeeding.GetTopUpOptions());
            transactionDbContext.SaveChanges();
        }
    }

    public static void UnSeedTransactionTopUpOptions()
    {
        TransactionDbContext transactionDbContext = GetService<TransactionDbContext>();
        if (transactionDbContext.TopUpOptions.Any())
        {
            transactionDbContext.TopUpOptions.RemoveRange(transactionDbContext.TopUpOptions.ToList());
            transactionDbContext.SaveChanges();
        }
    }
    #endregion

    #region TopupGenie Transaction
    public static void SeedTopupGenieTransaction()
    {
        SeedTopupGenieBeneficiary();
        SeedTopupGenieTopUpOptions();

        TopUpGenieDbContext topUpGenieDbContext = GetService<TopUpGenieDbContext>();
        if (!topUpGenieDbContext.Transactions.Any())
        {
            topUpGenieDbContext.Transactions.AddRange(TransactionSeeding.GetTransaction());
            topUpGenieDbContext.SaveChanges();
        }
    }

    public static void UnSeedTopupGenieTransaction()
    {
        TopUpGenieDbContext topUpGenieDbContext = GetService<TopUpGenieDbContext>();
        if (topUpGenieDbContext.Transactions.Any())
        {
            topUpGenieDbContext.Transactions.RemoveRange(topUpGenieDbContext.Transactions.ToList());
            topUpGenieDbContext.SaveChanges();
        }
    }
    #endregion

    #region Transaction Seed
    public static void SeedTransaction()
    {
        SeedTransactionBeneficiary();
        SeedTransactionTopUpOptions();
        TransactionDbContext transactionDbContext = GetService<TransactionDbContext>();
        
        if (!transactionDbContext.Transactions.Any())
        {
            transactionDbContext.Transactions.AddRange(TransactionSeeding.GetTransaction());
            transactionDbContext.SaveChanges();
        }
    }

    public static void UnSeedTransaction()
    {
        TransactionDbContext transactionDbContext = GetService<TransactionDbContext>();
        
        if (transactionDbContext.Transactions.Any())
        {
            transactionDbContext.Transactions.RemoveRange(transactionDbContext.Transactions.ToList());
            transactionDbContext.SaveChanges();
        }
    }
    #endregion
}

