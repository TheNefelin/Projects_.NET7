namespace ProjectPasswordManager.Application.DTOs;

public class CoreUserRequest
{
    public required Guid IdUser { get; set; }
    public required Guid SqlToken { get; set; }
    public required string Password { get; set; } = string.Empty;
}
