namespace TopUpGenie.Services.Helper;

public static class PasswordHelper
{
    private static readonly PasswordHasher<object> _passwordHasher = new PasswordHasher<object>();
    private static readonly object _dummy = new object();

    public static string GenerateHash(string password)
	{
		return _passwordHasher.HashPassword(_dummy, password);
    }

    public static bool VerifyPassword(string? hashedPassword, string? providedPassword)
    {
        PasswordVerificationResult result = _passwordHasher.VerifyHashedPassword(_dummy, hashedPassword, providedPassword);

        return result == PasswordVerificationResult.Success;
    }
}