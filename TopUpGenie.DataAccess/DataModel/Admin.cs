namespace TopUpGenie.DataAccess.DataModel;

public class Admin : User
{
    [Required]
    public bool IsAdmin { get; set; } = false;
}