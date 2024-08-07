namespace TopUpGenie.Services.Constants;

/// <summary>
/// ErrorMessage
/// </summary>
public static class ErrorMessage
{
    #region Admin Error Messages

    /// <summary>
    /// ADMIN_CREATE_USER_FAILED
    /// </summary>
    public const string ADMIN_CREATE_USER_FAILED = "Create user failed";

    /// <summary>
    /// ADMIN_CREATE_USER_EXCEPTION
    /// </summary>
    public const string ADMIN_CREATE_USER_EXCEPTION = "Exception occured during user creation - {0}";

    /// <summary>
    /// ADMIN_GET_USER_BY_ID_EXCEPTION
    /// </summary>
    public const string ADMIN_GET_USER_BY_ID_EXCEPTION = "Exception occured on quering users by id - {0}";

    /// <summary>
    /// ADMIN_GET_USER_BY_ID_FAILED
    /// </summary>
    public const string ADMIN_GET_USER_BY_ID_FAILED = "Get user by id failed";

    /// <summary>
    /// ADMIN_GET_ALL_USERS_EXCEPTION
    /// </summary>
    public const string ADMIN_GET_ALL_USERS_EXCEPTION = "Exception occured on quering all users - {0}";

    /// <summary>
    /// ADMIN_GET_ALL_USERS_FAILED
    /// </summary>
    public const string ADMIN_GET_ALL_USERS_FAILED = "Get all users failed";

    /// <summary>
    /// ADMIN_UPDATE_USER_EXCEPTION
    /// </summary>
    public const string ADMIN_UPDATE_USER_EXCEPTION = "Exception occured when updating an user - {0}";

    /// <summary>
    /// ADMIN_GET_ALL_USERS_FAILED
    /// </summary>
    public const string ADMIN_UPDATE_USER_FAILED = "Update user failed";

    /// <summary>
    /// ADMIN_DELETE_USER_EXCEPTION
    /// </summary>
    public const string ADMIN_DELETE_USER_EXCEPTION = "Exception occured when deleting an user - {0}";

    /// <summary>
    /// ADMIN_DELETE_USER_FAILED
    /// </summary>
    public const string ADMIN_DELETE_USER_FAILED = "Delete user failed. User is not allowed for deletion or does not exist";

    /// <summary>
    /// ADMIN_GET_LAST_FIVE_TRANSACTION_EXCEPTION
    /// </summary>
    public const string ADMIN_GET_LAST_FIVE_TRANSACTION_EXCEPTION = "Exception occured when fetching transaction records - {0}";

    /// <summary>
    /// ADMIN_GET_LAST_FIVE_TRANSACTION_FAILED
    /// </summary>
    public const string ADMIN_GET_LAST_FIVE_TRANSACTION_FAILED = "Failed to fetch from transaction records";

    /// <summary>
    /// ADMIN_PASSWORD_CHANGE_USER_NOT_FOUND
    /// </summary>
    public const string ADMIN_PASSWORD_CHANGE_USER_NOT_FOUND = "Password change not initiated. User not found";

    /// <summary>
    /// ADMIN_PASSWORD_CHANGE_NOT_INITIATED
    /// </summary>
    public const string ADMIN_PASSWORD_CHANGE_NOT_INITIATED = "Password change not initiated";

    /// <summary>
    /// ADMIN_PASSWORD_CHANGE_MISMATCH
    /// </summary>
    public const string ADMIN_PASSWORD_CHANGE_MISMATCH = "Password change not initiated. New password and Confirm password mismatch";

    /// <summary>
    /// ADMIN_PASSWORD_CHANGE_INCORRECT_OLD_PASSWORD
    /// </summary>
    public const string ADMIN_PASSWORD_CHANGE_INCORRECT_OLD_PASSWORD = "Password change not initiated. Incorrect old password";

    #endregion

    #region Authentication Error Messages

    /// <summary>
    /// AUTHENTICATION_EXCEPTION
    /// </summary>
    public const string AUTHENTICATION_EXCEPTION = "Exception occured when validating user - {0}";

    /// <summary>
    /// AUTHENTICATION_FAILED
    /// </summary>
    public const string AUTHENTICATION_FAILED = "Authentication failed. UserId or Password is incorrect";

    /// <summary>
    /// AUTHENTICATION_TOKEN_GENERATION_FAILED
    /// </summary>
    public const string AUTHENTICATION_TOKEN_GENERATION_FAILED = "Authentication failed. Failed to generate Token";

    /// <summary>
    /// AUTHENTICATION_INVALIDATION_EXCEPTION
    /// </summary>
    public const string AUTHENTICATION_INVALIDATION_EXCEPTION = "Exception occured when invalidating user session - {0}";

    /// <summary>
    /// AUTHENTICATION_INVALIDATION_FAILED
    /// </summary>
    public const string AUTHENTICATION_INVALIDATION_FAILED = "Failed to invalidate user";

    #endregion

    #region Token Error Messages

    /// <summary>
    /// TOKEN_SERVICE_FAILED
    /// </summary>
    public const string TOKEN_SERVICE_FAILED = "Failed to invalidate user session or user session does not exist";

    /// <summary>
    /// TOKEN_SERVICE_EXISTING_SESSION
    /// </summary>
    public const string TOKEN_SERVICE_EXISTING_SESSION = "User session is still active. Terminating existing session";

    /// <summary>
    /// TOKEN_SERVICE_EXCEPTION
    /// </summary>
    public const string TOKEN_SERVICE_EXCEPTION = "Exception occured when creating user session - {0}";

    /// <summary>
    /// TOKEN_SERVICE_FAILED
    /// </summary>
    public const string TOKEN_INVALIDATION_FAILED = "Failed to delete user session";

    /// <summary>
    /// TOKEN_SERVICE_EXCEPTION
    /// </summary>
    public const string TOKEN_INVALIDATION_EXCEPTION = "Exception occured when deleteing user session - {0}";

    /// <summary>
    /// TOKEN_VALIDATION_FAILED
    /// </summary>
    public const string TOKEN_VALIDATION_FAILED = "Invalid Token";

    /// <summary>
    /// TOKEN_VALIDATION_EXCEPTION
    /// </summary>
    public const string TOKEN_VALIDATION_EXCEPTION = "Token validation failed with exception - {0}";

    /// <summary>
    /// TOKEN_REFRESH_FAILED
    /// </summary>
    public const string TOKEN_REFRESH_FAILED = "Failed to refresh token";

    #endregion

    #region Beneficiary Error Messages

    // ActivateMyBeneficiary
    /// <summary>
    /// BENEFICIARY_ACTIVATE_FAILED
    /// </summary>
    public const string BENEFICIARY_ACTIVATE_FAILED = "Activate beneficiary failed";

    /// <summary>
    /// BENEFICIARY_ACTIVATE_EXCEPTION
    /// </summary>
    public const string BENEFICIARY_ACTIVATE_EXCEPTION = "Exception occurred during beneficiary activation - {0}";

    // CreateBeneficiaryAsync
    /// <summary>
    /// BENEFICIARY_CREATE_FIND_FAILED
    /// </summary>
    public const string BENEFICIARY_CREATE_LIMIT_EXCEEDED_FAILED = "Limit exceeded for total active beneficiaries";

    /// <summary>
    /// BENEFICIARY_CREATE_ADD_FAILED
    /// </summary>
    public const string BENEFICIARY_CREATE_ADD_FAILED = "Add beneficiary failed";

    /// <summary>
    /// BENEFICIARY_CREATE_EXCEPTION
    /// </summary>
    public const string BENEFICIARY_CREATE_EXCEPTION = "Exception occurred during beneficiary creation - {0}";

    // DeleteMyBeneficiary
    /// <summary>
    /// BENEFICIARY_DELETE_FAILED
    /// </summary>
    public const string BENEFICIARY_DELETE_FAILED = "Delete beneficiary failed";

    /// <summary>
    /// BENEFICIARY_DELETE_EXCEPTION
    /// </summary>
    public const string BENEFICIARY_DELETE_EXCEPTION = "Exception occurred during beneficiary deletion - {0}";

    // GetMyBeneficiaries
    /// <summary>
    /// BENEFICIARY_GET_ALL_FAILED
    /// </summary>
    public const string BENEFICIARY_GET_ALL_FAILED = "Get all beneficiaries failed";

    /// <summary>
    /// BENEFICIARY_GET_ALL_EXCEPTION
    /// </summary>
    public const string BENEFICIARY_GET_ALL_EXCEPTION = "Exception occurred during getting all beneficiaries - {0}";

    // GetMyBeneficiaryById
    /// <summary>
    /// BENEFICIARY_GET_BY_ID_FAILED
    /// </summary>
    public const string BENEFICIARY_GET_BY_ID_FAILED = "Get beneficiary by id failed";

    /// <summary>
    /// BENEFICIARY_GET_BY_ID_EXCEPTION
    /// </summary>
    public const string BENEFICIARY_GET_BY_ID_EXCEPTION = "Exception occurred during getting beneficiary by id - {0}";

    // UpdateMyBeneficiary
    /// <summary>
    /// BENEFICIARY_UPDATE_FIND_FAILED
    /// </summary>
    public const string BENEFICIARY_UPDATE_FIND_FAILED = "Failed to find beneficiary for update";

    /// <summary>
    /// BENEFICIARY_UPDATE_FAILED
    /// </summary>
    public const string BENEFICIARY_UPDATE_FAILED = "Update beneficiary failed";

    /// <summary>
    /// BENEFICIARY_UPDATE_EXCEPTION
    /// </summary>
    public const string BENEFICIARY_UPDATE_EXCEPTION = "Exception occurred during beneficiary update - {0}";

    #endregion

    #region Profile Error Messages

    /// <summary>
    /// PROFILE_GET_FAILED
    /// </summary>
    public const string PROFILE_GET_FAILED = "Get profile failed";

    /// <summary>
    /// PROFILE_GET_EXCEPTION
    /// </summary>
    public const string PROFILE_GET_EXCEPTION = "Exception occurred during profile retrieval - {0}";

    /// <summary>
    /// PROFILE_NO_TRANSACTION_RECORDS
    /// </summary>
    public const string PROFILE_NO_TRANSACTION_RECORDS = "No transaction records found for the profile";

    /// <summary>
    /// PROFILE_NO_BENEFICIARIES
    /// </summary>
    public const string PROFILE_NO_BENEFICIARIES = "No beneficiaries found for the profile";

    #endregion

    #region Transaction Error Messages

    /// <summary>
    /// TRANSACTION_MONTHLY_LIMIT_REACHED
    /// </summary>
    public const string TRANSACTION_MONTHLY_LIMIT_REACHED = "Monthly Transaction Limit Reached";

    /// <summary>
    /// TRANSACTION_INSUFICIENT_BALANCE
    /// </summary>
    public const string TRANSACTION_INSUFICIENT_BALANCE = "Insufficient Balance";

    /// <summary>
    /// TRANSACTION_DEBIT_FAILED
    /// </summary>
    public const string TRANSACTION_DEBIT_FAILED = "Failed to debt user account";

    /// <summary>
    /// TRANSACTION_CREDIT_FAILED
    /// </summary>
    public const string TRANSACTION_CREDIT_FAILED = "Failed to credit user account";

    #endregion

    #region Topup Error Messages

    /// <summary>
    /// TOPUP_LIST_OPTIONS_FAILED
    /// </summary>
    public static string TOPUP_LIST_OPTIONS_FAILED = "Listing top-up options failed";

    /// <summary>
    /// TOPUP_LIST_OPTIONS_EXCEPTION
    /// </summary>
    public static string TOPUP_LIST_OPTIONS_EXCEPTION = "Exception occurred while listing top-up options - {0}";

    // TopUpTransaction
    /// <summary>
    /// TOPUP_TRANSACTION_FAILED
    /// </summary>
    public static string TOPUP_TRANSACTION_FAILED = "Top-up transaction failed";

    /// <summary>
    /// TOPUP_TRANSACTION_EXCEPTION
    /// </summary>
    public static string TOPUP_TRANSACTION_EXCEPTION = "Exception occurred during top-up transaction - {0}";

    #endregion
}