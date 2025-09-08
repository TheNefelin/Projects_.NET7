using System.ComponentModel.DataAnnotations;

namespace ProjectPasswordManager.Domain.Entities;

public class CoreData
{
    [Key]
    public int Id { get; set; }
    [MaxLength(256)]
    public required string Data01 { get; set; }
    [MaxLength(256)]
    public required string Data02 { get; set; }
    [MaxLength(256)]
    public required string Data03 { get; set; }
    public required Guid IdUser { get; set; }
}
