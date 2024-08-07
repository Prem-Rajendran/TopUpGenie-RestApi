namespace TopUpGenie.Services.Helper;

/// <summary>
/// PasswordHelper
/// </summary>
public static class PasswordHelper
{
    private static readonly PasswordHasher<object> _passwordHasher = new PasswordHasher<object>();
    private static readonly object _dummy = new object();

    /// <summary>
    /// GenerateHash
    /// </summary>
    /// <param name="password"></param>
    /// <returns></returns>
    public static string GenerateHash(string password)
	{
		return _passwordHasher.HashPassword(_dummy, password);
    }

    /// <summary>
    /// VerifyPassword
    /// </summary>
    /// <param name="hashedPassword"></param>
    /// <param name="providedPassword"></param>
    /// <returns></returns>
    public static bool VerifyPassword(string? hashedPassword, string? providedPassword)
    {
        if (!string.IsNullOrWhiteSpace(hashedPassword) && !string.IsNullOrWhiteSpace(providedPassword))
        {
            PasswordVerificationResult result = _passwordHasher.VerifyHashedPassword(_dummy, hashedPassword ?? "", providedPassword ?? "");
            return result == PasswordVerificationResult.Success;
        }

        return false;
    }
}