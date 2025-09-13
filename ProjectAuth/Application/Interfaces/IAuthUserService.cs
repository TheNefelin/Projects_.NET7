using Core;
using ProjectAuth.Application.DTOs;

namespace ProjectAuth.Application.Interfaces;

public interface IAuthUserService
{
    Task<ApiResponse<AuthUserResponse>> RegisterAsync(AuthUserRegister authUserRegister, CancellationToken cancellationToken);
}
