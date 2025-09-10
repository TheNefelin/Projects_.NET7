namespace ProjectPasswordManager.Domain.Entities;

public class CoreData
{
    public int Data_Id { get; set; }
    public string? Data01 { get; set; }
    public string? Data02 { get; set; }
    public string? Data03 { get; set; }
    public required Guid User_Id { get; set; }
}
