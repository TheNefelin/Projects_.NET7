using System.ComponentModel.DataAnnotations;

namespace ProjectGamesGuide.Application.DTOs;

public class LoginGoogle
{
    [EmailAddress]
    [MaxLength(100)]
    public required string Email { get; set; }
    public required string Sub { get; set; }
    public required string Jti { get; set; }
}
