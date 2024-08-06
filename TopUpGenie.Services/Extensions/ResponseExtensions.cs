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
                session = user.LoginSessions?.FirstOrDefault();

                if (session == null)
                {
                    return new LoginSession
                    {
                        AccessToken = responseModel.AccessToken,
                        RefreshToken = responseModel.RefreshToken,
                        ExpirationDateTime = responseModel.Expiration,
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now,
                        UserId = user.Id,
                        User = user
                    };
                }
                else
                {
                    session.AccessToken = responseModel.AccessToken;
                    session.RefreshToken = responseModel.RefreshToken;
                    session.ExpirationDateTime = responseModel.Expiration;
                    session.UpdatedAt = DateTime.Now;
                }
            }

            return session;
        }
    }
}

