using Core;
using ProjectGamesGuide.Application.DTOs;

namespace ProjectGamesGuide.Application.Interfaces;

public interface IAuthGoogleService
{
    Task<ApiResponse<LoggedToken>> LoginGoogleAsync(LoginGoogle login, CancellationToken cancellationToken);
}
