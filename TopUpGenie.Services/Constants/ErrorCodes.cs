namespace TopUpGenie.Services.Constants;

/// <summary>
/// ErrorCodes
/// </summary>
[ExcludeFromCodeCoverage]
public static class ErrorCodes
{
    #region Admin Error Codes

    /// <summary>
    /// ADMIN_CREATE_USER_FAILED
    /// </summary>
    public const string ADMIN_CREATE_USER_FAILED = "ADMIN-001";

    /// <summary>
    /// ADMIN_CREATE_USER_EXCEPTION
    /// </summary>
    public const string ADMIN_CREATE_USER_EXCEPTION = "ADMIN-002";

    /// <summary>
    /// ADMIN_GET_USER_BY_ID_EXCEPTION
    /// </summary>
    public const string ADMIN_GET_USER_BY_ID_EXCEPTION = "ADMIN-003";

    /// <summary>
    /// ADMIN_GET_USER_BY_ID_FAILED
    /// </summary>
    public const string ADMIN_GET_USER_BY_ID_FAILED = "ADMIN-004";

    /// <summary>
    /// ADMIN_GET_ALL_USERS_EXCEPTION
    /// </summary>
    public const string ADMIN_GET_ALL_USERS_EXCEPTION = "ADMIN-005";

    /// <summary>
    /// ADMIN_GET_ALL_USERS_FAILED
    /// </summary>
    public const string ADMIN_GET_ALL_USERS_FAILED = "ADMIN-006";

    /// <summary>
    /// ADMIN_UPDATE_USER_EXCEPTION
    /// </summary>
    public const string ADMIN_UPDATE_USER_EXCEPTION = "ADMIN-007";

    /// <summary>
    /// ADMIN_UPDATE_USER_FAILED
    /// </summary>
    public const string ADMIN_UPDATE_USER_FAILED = "ADMIN-008";

    /// <summary>
    /// ADMIN_DELETE_USER_EXCEPTION
    /// </summary>
    public const string ADMIN_DELETE_USER_EXCEPTION = "ADMIN-009";

    /// <summary>
    /// ADMIN_DELETE_USER_FAILED
    /// </summary>
    public const string ADMIN_DELETE_USER_FAILED = "ADMIN-010";

    /// <summary>
    /// ADMIN_GET_LAST_FIVE_TRANSACTION_FAILED
    /// </summary>
    public const string ADMIN_GET_LAST_FIVE_TRANSACTION_FAILED = "ADMIN-011";

    /// <summary>
    /// ADMIN_GET_LAST_FIVE_TRANSACTION_EXCEPTION
    /// </summary>
    public const string ADMIN_GET_LAST_FIVE_TRANSACTION_EXCEPTION = "ADMIN-012";

    /// <summary>
    /// ADMIN_PASSWORD_CHANGE_USER_NOT_FOUND
    /// </summary>
    public const string ADMIN_PASSWORD_CHANGE_USER_NOT_FOUND = "ADMIN-013";

    /// <summary>
    /// ADMIN_PASSWORD_CHANGE_NOT_INITIATED
    /// </summary>
    public const string ADMIN_PASSWORD_CHANGE_NOT_INITIATED = "ADMIN-014";

    /// <summary>
    /// ADMIN_PASSWORD_CHANGE_MISMATCH
    /// </summary>
    public const string ADMIN_PASSWORD_CHANGE_MISMATCH = "ADMIN-015";

    /// <summary>
    /// ADMIN_PASSWORD_CHANGE_INCORRECT_OLD_PASSWORD
    /// </summary>
    public const string ADMIN_PASSWORD_CHANGE_INCORRECT_OLD_PASSWORD = "ADMIN-016";

    #endregion

    #region Authentication Error Codes

    /// <summary>
    /// AUTHENTICATION_EXCEPTION
    /// </summary>
    public const string AUTHENTICATION_EXCEPTION = "AUTHENTICATION-001";

    /// <summary>
    /// AUTHENTICATION_FAILED
    /// </summary>
    public const string AUTHENTICATION_FAILED = "AUTHENTICATION-002";

    /// <summary>
    /// AUTHENTICATION_TOKEN_GENERATION_FAILED
    /// </summary>
    public const string AUTHENTICATION_TOKEN_GENERATION_FAILED = "AUTHENTICATION-003";

    /// <summary>
    /// AUTHENTICATION_INVALIDATION_EXCEPTION
    /// </summary>
    public const string AUTHENTICATION_INVALIDATION_EXCEPTION = "AUTHENTICATION-003";

    /// <summary>
    /// AUTHENTICATION_INVALIDATION_FAILED
    /// </summary>
    public const string AUTHENTICATION_INVALIDATION_FAILED = "AUTHENTICATION-004";

    #endregion

    #region Token Error Codes

    /// <summary>
    /// TOKEN_SERVICE_FAILED
    /// </summary>
    public const string TOKEN_SERVICE_FAILED = "TOKEN-SERVICE-001";

    /// <summary>
    /// TOKEN_SERVICE_EXISTING_SESSION
    /// </summary>
    public const string TOKEN_SERVICE_EXISTING_SESSION = "TOKEN-SERVICE-002";

    /// <summary>
    /// TOKEN_SERVICE_EXCEPTION
    /// </summary>
    public const string TOKEN_SERVICE_EXCEPTION = "TOKEN-SERVICE-003";

    /// <summary>
    /// TOKEN_SERVICE_FAILED
    /// </summary>
    public const string TOKEN_INVALIDATION_FAILED = "TOKEN-SERVICE-004";

    /// <summary>
    /// TOKEN_SERVICE_EXCEPTION
    /// </summary>
    public const string TOKEN_INVALIDATION_EXCEPTION = "TOKEN-SERVICE-005";

    /// <summary>
    /// TOKEN_SERVICE_FAILED
    /// </summary>
    public const string TOKEN_VALIDATION_FAILED = "TOKEN-SERVICE-006";

    /// <summary>
    /// TOKEN_SERVICE_EXCEPTION
    /// </summary>
    public const string TOKEN_VALIDATION_EXCEPTION = "TOKEN-SERVICE-007";

    /// <summary>
    /// TOKEN_REFRESH_FAILED
    /// </summary>
    public const string TOKEN_REFRESH_FAILED = "TOKEN-SERVICE-008";

    #endregion

    #region Beneficiary Error Codes

    /// <summary>
    /// BENEFICIARY_ACTIVATE_FAILED
    /// </summary>
    public const string BENEFICIARY_ACTIVATE_FAILED = "BENEFICIARY-001";

    /// <summary>
    /// BENEFICIARY_ACTIVATE_EXCEPTION
    /// </summary>
    public const string BENEFICIARY_ACTIVATE_EXCEPTION = "BENEFICIARY-002";

    // CreateBeneficiaryAsync
    /// <summary>
    /// BENEFICIARY_CREATE_FIND_FAILED
    /// </summary>
    public const string BENEFICIARY_CREATE_LIMIT_EXCEEDED_FAILED = "BENEFICIARY-003";

    /// <summary>
    /// BENEFICIARY_CREATE_ADD_FAILED
    /// </summary>
    public const string BENEFICIARY_CREATE_ADD_FAILED = "BENEFICIARY-004";

    /// <summary>
    /// BENEFICIARY_CREATE_EXCEPTION
    /// </summary>
    public const string BENEFICIARY_CREATE_EXCEPTION = "BENEFICIARY-005";

    /// <summary>
    /// BENEFICIARY_DELETE_FAILED
    /// </summary>
    public const string BENEFICIARY_DELETE_FAILED = "BENEFICIARY-006";

    /// <summary>
    /// BENEFICIARY_DELETE_EXCEPTION
    /// </summary>
    public const string BENEFICIARY_DELETE_EXCEPTION = "BENEFICIARY-007";

    /// <summary>
    /// BENEFICIARY_GET_ALL_FAILED
    /// </summary>
    public const string BENEFICIARY_GET_ALL_FAILED = "BENEFICIARY-008";

    /// <summary>
    /// BENEFICIARY_GET_ALL_EXCEPTION
    /// </summary>
    public const string BENEFICIARY_GET_ALL_EXCEPTION = "BENEFICIARY-009";

    /// <summary>
    /// BENEFICIARY_GET_BY_ID_FAILED
    /// </summary>
    public const string BENEFICIARY_GET_BY_ID_FAILED = "BENEFICIARY-010";

    /// <summary>
    /// BENEFICIARY_GET_BY_ID_EXCEPTION
    /// </summary>
    public const string BENEFICIARY_GET_BY_ID_EXCEPTION = "BENEFICIARY-011";

    /// <summary>
    /// BENEFICIARY_UPDATE_FIND_FAILED
    /// </summary>
    public const string BENEFICIARY_UPDATE_FIND_FAILED = "BENEFICIARY-012";

    /// <summary>
    /// BENEFICIARY_UPDATE_FAILED
    /// </summary>
    public const string BENEFICIARY_UPDATE_FAILED = "BENEFICIARY-013";

    /// <summary>
    /// BENEFICIARY_UPDATE_EXCEPTION
    /// </summary>
    public const string BENEFICIARY_UPDATE_EXCEPTION = "BENEFICIARY-014";

    #endregion

    #region Profile Error Codes

    /// <summary>
    /// PROFILE_GET_FAILED
    /// </summary>
    public const string PROFILE_GET_FAILED = "PROFILE-001";

    /// <summary>
    /// PROFILE_GET_EXCEPTION
    /// </summary>
    public const string PROFILE_GET_EXCEPTION = "PROFILE-002";

    /// <summary>
    /// PROFILE_NO_TRANSACTION_RECORDS
    /// </summary>
    public const string PROFILE_NO_TRANSACTION_RECORDS = "PROFILE-003";

    /// <summary>
    /// PROFILE_NO_BENEFICIARIES
    /// </summary>
    public const string PROFILE_NO_BENEFICIARIES = "PROFILE-004";

    #endregion

    #region Transaction Error Codes

    /// <summary>
    /// TRANSACTION_MONTHLY_LIMIT_REACHED
    /// </summary>
    public const string TRANSACTION_MONTHLY_LIMIT_REACHED = "TRANSACTION-001";

    /// <summary>
    /// TRANSACTION_INSUFICIENT_BALANCE
    /// </summary>
    public const string TRANSACTION_INSUFICIENT_BALANCE = "TRANSACTION-002";

    /// <summary>
    /// TRANSACTION_DEBIT_FAILED
    /// </summary>
    public const string TRANSACTION_DEBIT_FAILED = "TRANSACTION-003";

    /// <summary>
    /// TRANSACTION_CREDIT_FAILED
    /// </summary>
    public const string TRANSACTION_CREDIT_FAILED = "TRANSACTION-004";

    #endregion

    #region Topup Error Codes

    /// <summary>
    /// TOPUP_LIST_OPTIONS_FAILED
    /// </summary>
    public const string TOPUP_LIST_OPTIONS_FAILED = "TOPUP-001";

    /// <summary>
    /// TOPUP_LIST_OPTIONS_EXCEPTION
    /// </summary>
    public const string TOPUP_LIST_OPTIONS_EXCEPTION = "TOPUP-002";

    /// <summary>
    /// TOPUP_TRANSACTION_FAILED
    /// </summary>
    public const string TOPUP_TRANSACTION_FAILED = "TOPUP-003";

    /// <summary>
    /// TOPUP_TRANSACTION_EXCEPTION
    /// </summary>
    public const string TOPUP_TRANSACTION_EXCEPTION = "TOPUP-004";

    #endregion
}