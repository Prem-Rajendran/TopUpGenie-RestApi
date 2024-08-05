﻿namespace TopUpGenie.DataAccess.DataModel;

public class User
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string? Name { get; set; }

    [Required]
    public string? Password { get; set; }

    [Required]
    public bool Verified { get; set; } = false;

    [Required]
    public bool IsAdmin { get; set; } = false;

    public ICollection<Account>? Accounts { get; set; }
}