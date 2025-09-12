namespace ProjectAuth.Infrastructure.Models;

public class JwtUser
{
    public required string IdUser { get; set; }
    public required string Email { get; set; }
    public required string Role { get; set; }
}
