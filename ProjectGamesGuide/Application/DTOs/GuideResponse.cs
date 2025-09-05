using ProjectGamesGuide.Domain.Entities;

namespace ProjectGamesGuide.Application.DTOs;

public class GuideResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Sort { get; set; }
    public GuideUser GuideUser { get; set; }
    public ICollection<AdventureResponse> Adventures { get; set; }
}
