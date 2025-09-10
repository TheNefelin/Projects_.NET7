namespace ProjectPasswordManager.Application.DTOs;

public class CoreDataPassword
{
    public required string Password { get; set; }
    public required CoreUserRequest CoreUser { get; set; }
}
