namespace ProjectPasswordManager.Application.DTOs;

public class UserRequest<T>
{
    public required string IdUser { get; set; }
    public required string SqlToken { get; set; }
    public string Password { get; set; } = string.Empty;
    public T? CoreData { get; set; } // Solo para operaciones de inserción/actualización
}
