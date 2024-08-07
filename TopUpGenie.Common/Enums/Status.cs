namespace TopUpGenie.Common.Enums;

public enum Status
{
    [Description("Success")]
    Success,

    [Description("PartialSuccess")]
    PartialSuccess,

    [Description("Failure")]
    Failure,

    [Description("Unknown")]
    Unknown
}