namespace ProjectGamesGuide.Domain.Entities;

public class Source
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
    public int Id_Game { get; set; }
}
