using Core;
using ProjectPasswordManager.Application.DTOs;

namespace ProjectPasswordManager.Application.Interfaces;

public interface ICoreUserService
{
    Task<ApiResponse<CoreUserIV>> RegisterCoreUserPasswordAsync(CoreUserRequest coreUserRequest, CancellationToken cancellationToken);
    Task<ApiResponse<CoreUserIV>> GetCoreUserIVAsync(CoreUserRequest coreUserRequest, CancellationToken cancellationToken);
}
