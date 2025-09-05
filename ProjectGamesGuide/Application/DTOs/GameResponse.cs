using ProjectGamesGuide.Domain.Entities;

namespace ProjectGamesGuide.Application.DTOs;

public class GameResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string ImgUrl { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public ICollection<CharacterResponse> Characters { get; set; }
    public ICollection<SourceResponse> Sources { get; set; }
    public ICollection<BackgroundImgResponse> BackgroundImgs { get; set; }
    public ICollection<GuideResponse> guides { get; set; }
}
