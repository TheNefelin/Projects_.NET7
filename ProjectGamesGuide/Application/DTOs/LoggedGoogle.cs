namespace ProjectGamesGuide.Application.DTOs;

public class LoggedGoogle
{
    public required bool IsSucces { get; set; }
    public required int StatusCode { get; set; }
    public required string Msge { get; set; }
    public string Id { get; set; } = string.Empty;
    public string SqlToken { get; set; } = string.Empty;
}
