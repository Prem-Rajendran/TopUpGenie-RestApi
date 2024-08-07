namespace TopUpGenie.DataAccess.DataModel;

/// <summary>
/// TopUpOption
/// </summary>
[ExcludeFromCodeCoverage]
public class TopUpOption
{
    [Key]
    public int TopUpOptionId { get; set; }

    [Required]
    public int Amount { get; set; }

    [Required]
    public string? Description { get; set; }
}