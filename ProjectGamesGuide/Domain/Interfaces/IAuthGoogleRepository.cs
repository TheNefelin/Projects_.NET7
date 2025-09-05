using ProjectGamesGuide.Application.DTOs;

namespace ProjectGamesGuide.Domain.Interfaces;

public interface IAuthGoogleRepository
{
    Task<LoggedGoogle> LoginGoogleAsync(LoginGoogle login, CancellationToken cancellationToken);
}
