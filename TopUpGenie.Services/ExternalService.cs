namespace TopUpGenie.Services;

/// <summary>
/// ExternalService
/// </summary>
[ExcludeFromCodeCoverage]
public class ExternalService : IExternalService
{
    private readonly HttpClient _httpClient;

    public ExternalService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    /// <summary>
    /// CreditUserAccountAsync
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="amount"></param>
    /// <returns></returns>
    public async Task<bool> CreditUserAccountAsync(int userId, int amount)
    {
        var payload = new { userId, amount };
        var response = await _httpClient.PostAsJsonAsync("https://api.externalservice.com/user/credit", payload);
        return response.IsSuccessStatusCode;
    }

    /// <summary>
    /// DebitUserAccountAsync
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="amount"></param>
    /// <returns></returns>
    public async Task<bool> DebitUserAccountAsync(int userId, int amount)
    {
        var payload = new { userId, amount };
        var response = await _httpClient.PostAsJsonAsync("https://api.externalservice.com/user/debit", payload);
        return response.IsSuccessStatusCode;
    }

    /// <summary>
    /// GetUserBalanceAsync
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    public async Task<int> GetUserBalanceAsync(int userId)
    {
        var response = await _httpClient.GetAsync($"https://api.externalservice.com/user/balance?userId={userId}");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<int>();
    }
}

