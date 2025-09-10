namespace ProjectPasswordManager.Application.DTOs;

public class CoreDataDelete
{
    public required int Data_Id { get; set; }
    public required CoreUserRequest CoreUser { get; set; }
}
