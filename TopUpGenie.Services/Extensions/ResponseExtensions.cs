using System;
namespace TopUpGenie.Services.Extensions
{
	public static class ResponseExtensions
	{
        public static LoginSession? ToLoginSession(this TokenResponseModel responseModel, User user)
        {
            LoginSession? session = null;

            if (responseModel != null && user != null)
            {
                session = new LoginSession
                {
                    AccessToken = responseModel.AccessToken,
                    RefreshToken = responseModel.RefreshToken,
                    ExpirationDateTime = responseModel.Expiration.AddHours(1),
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    UserId = user.Id,
                    User = user
                };
            }

            return session;
        }
    }
}

