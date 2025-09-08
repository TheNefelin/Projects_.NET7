using System.ComponentModel.DataAnnotations;

namespace ProjectPasswordManager.Domain.Entities;

public class CoreUser
{
    public required Guid IdUser { get; set; }
    public required string SqlToken { get; set; }   
    public string? HashPM { get; set; }
    public string? SaltPM { get; set; }
}
