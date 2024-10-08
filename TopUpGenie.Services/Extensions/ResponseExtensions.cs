﻿namespace TopUpGenie.Services.Extensions;

/// <summary>
/// ResponseExtensions
/// </summary>
public static class ResponseExtensions
{
    /// <summary>
    /// ToLoginSession
    /// </summary>
    /// <param name="responseModel"></param>
    /// <param name="user"></param>
    /// <returns></returns>
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

