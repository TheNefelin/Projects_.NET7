namespace ProjectGamesGuide.Domain.Entities;

public class BackgroundImg
{
    public int Id { get; set; }
    public string ImgUrl { get; set; } = string.Empty;
    public int Id_Game { get; set; }
}
