namespace ProjectGamesGuide.Domain.Entities;

public class Guide
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Sort { get; set; }
    public int Id_Game { get; set; }
}
