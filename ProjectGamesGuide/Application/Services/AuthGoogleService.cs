using Core;
using ProjectGamesGuide.Application.DTOs;
using ProjectGamesGuide.Application.Interfaces;
using ProjectGamesGuide.Domain.Entities;
using ProjectGamesGuide.Domain.Interfaces;

namespace ProjectGamesGuide.Application.Services;

public class AuthGoogleService : IAuthGoogleService
{
    private readonly IAuthGoogleRepository _authGoogleRepository;

    public AuthGoogleService(IAuthGoogleRepository authGoogleRepository)
    {
        _authGoogleRepository = authGoogleRepository;
    }

    public async Task<ApiResponse<LoggedToken>> LoginGoogleAsync(LoginGoogle login, CancellationToken cancellationToken)
    {
        try
        {
            var data = await _authGoogleRepository.LoginGoogleAsync(login, cancellationToken);
            return new ApiResponse<LoggedToken>
            {
                IsSuccess = data.IsSucces,
                StatusCode = data.StatusCode,
                Message = data.Msge,
                Data = new LoggedToken
                {
                    IdUser = data.Id,
                    SqlToken = data.SqlToken
                }
            };

        }
        catch (Exception ex)
        {
            return new ApiResponse<LoggedToken>
            {
                IsSuccess = false,
                StatusCode = 500,
                Message = ex.Message,
            };
        }
    }
}
