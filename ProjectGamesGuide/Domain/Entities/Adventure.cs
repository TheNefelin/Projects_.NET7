namespace ProjectGamesGuide.Domain.Entities;

public class Adventure
{
    public int Id { get; set; }
    public string Description { get; set; } = string.Empty;
    public bool IsImportant { get; set; }
    public int Sort { get; set; }
    public int Id_Guide { get; set; }
}
