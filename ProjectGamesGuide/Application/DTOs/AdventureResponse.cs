using ProjectGamesGuide.Domain.Entities;

namespace ProjectGamesGuide.Application.DTOs;

public class AdventureResponse
{
    public int Id { get; set; }
    public string Description { get; set; } = string.Empty;
    public bool IsImportant { get; set; }
    public int Sort { get; set; }
    public AdventureUser AdventureUser { get; set; }
    public ICollection<AdventureImgResponse> AdventureImg { get; set; }
}
