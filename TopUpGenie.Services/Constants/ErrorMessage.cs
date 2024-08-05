namespace TopUpGenie.Services.Constants;

public static class ErrorMessage
{
    /// <summary>
    /// ADMIN_CREATE_USER_FAILED
    /// </summary>
    public static string ADMIN_CREATE_USER_FAILED = "Create user failed";

    /// <summary>
    /// ADMIN_CREATE_USER_EXCEPTION
    /// </summary>
    public static string ADMIN_CREATE_USER_EXCEPTION = "Exception occured during user creation - {0}";

    /// <summary>
    /// ADMIN_GET_USER_BY_ID_EXCEPTION
    /// </summary>
    public static string ADMIN_GET_USER_BY_ID_EXCEPTION = "Exception occured on quering users by id - {0}";

    /// <summary>
    /// ADMIN_GET_USER_BY_ID_FAILED
    /// </summary>
    public static string ADMIN_GET_USER_BY_ID_FAILED = "Get user by id failed";

    /// <summary>
    /// ADMIN_GET_ALL_USERS_EXCEPTION
    /// </summary>
    public static string ADMIN_GET_ALL_USERS_EXCEPTION = "Exception occured on quering all users - {0}";

    /// <summary>
    /// ADMIN_GET_ALL_USERS_FAILED
    /// </summary>
    public static string ADMIN_GET_ALL_USERS_FAILED = "Get all users failed";

    /// <summary>
    /// ADMIN_UPDATE_USER_EXCEPTION
    /// </summary>
    public static string ADMIN_UPDATE_USER_EXCEPTION = "Exception occured when updating an user - {0}";

    /// <summary>
    /// ADMIN_GET_ALL_USERS_FAILED
    /// </summary>
    public static string ADMIN_UPDATE_USER_FAILED = "Update user failed";

    /// <summary>
    /// ADMIN_DELETE_USER_EXCEPTION
    /// </summary>
    public static string ADMIN_DELETE_USER_EXCEPTION = "Exception occured when deleting an user - {0}";

    /// <summary>
    /// ADMIN_DELETE_USER_FAILED
    /// </summary>
    public static string ADMIN_DELETE_USER_FAILED = "Delete user failed. User is not allowed for deletion or does not exist";

    /// <summary>
    /// AUTHENTICATION_EXCEPTION
    /// </summary>
    public static string AUTHENTICATION_EXCEPTION = "Exception occured when validating user - {0}";

    /// <summary>
    /// AUTHENTICATION_FAILED
    /// </summary>
    public static string AUTHENTICATION_FAILED = "Authentication failed. UserId or Password is incorrect";


    /// <summary>
    /// AUTHENTICATION_INVALIDATION_EXCEPTION
    /// </summary>
    public static string AUTHENTICATION_INVALIDATION_EXCEPTION = "Exception occured when invalidating user session - {0}";

    /// <summary>
    /// AUTHENTICATION_INVALIDATION_FAILED
    /// </summary>
    public static string AUTHENTICATION_INVALIDATION_FAILED = "Failed to invalidate user";

    /// <summary>
    /// TOKEN_SERVICE_FAILED
    /// </summary>
    public static string TOKEN_SERVICE_FAILED = "Failed to invalidate user session or user session does not exist";

    /// <summary>
    /// TOKEN_SERVICE_EXISTING_SESSION
    /// </summary>
    public static string TOKEN_SERVICE_EXISTING_SESSION = "User session is still active. Terminating existing session";

    /// <summary>
    /// TOKEN_SERVICE_EXCEPTION
    /// </summary>
    public static string TOKEN_SERVICE_EXCEPTION = "Exception occured when creating user session - {0}";

    /// <summary>
    /// TOKEN_SERVICE_FAILED
    /// </summary>
    public static string TOKEN_INVALIDATION_FAILED = "Failed to delete user session";

    /// <summary>
    /// TOKEN_SERVICE_EXCEPTION
    /// </summary>
    public static string TOKEN_INVALIDATION_EXCEPTION = "Exception occured when deleteing user session - {0}";

    /// <summary>
    /// TOKEN_VALIDATION_FAILED
    /// </summary>
    public static string TOKEN_VALIDATION_FAILED = "Invalid Token";

    /// <summary>
    /// TOKEN_VALIDATION_EXCEPTION
    /// </summary>
    public static string TOKEN_VALIDATION_EXCEPTION = "Token validation failed with exception - {0}";

}