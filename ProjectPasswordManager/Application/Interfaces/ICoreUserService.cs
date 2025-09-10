using Core;
using ProjectPasswordManager.Application.DTOs;

namespace ProjectPasswordManager.Application.Interfaces;

public interface ICoreUserService
{
    Task<ApiResponse<CoreUserIV>> RegisterCoreUserPasswordAsync(CoreDataPassword coreUserRequest, CancellationToken cancellationToken);
    Task<ApiResponse<CoreUserIV>> GetCoreUserIVAsync(CoreDataPassword coreUserRequest, CancellationToken cancellationToken);
}
