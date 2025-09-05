namespace ProjectGamesGuide.Domain.Entities;

public class AdventureImg
{
    public int Id { get; set; }
    public string ImgUrl { get; set; } = string.Empty;
    public int Sort { get; set; }
    public int Id_Adventure { get; set; }
}
