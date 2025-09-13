using Core;
using ProjectAuth.Application.DTOs;
using ProjectAuth.Application.Interfaces;
using ProjectAuth.Domain.Entities;
using ProjectAuth.Domain.Interfaces;
using ProjectAuth.Infrastructure.Services;

namespace ProjectAuth.Application.Services;

public class AuthUserService : IAuthUserService
{
    private readonly IAuthUserRepository _authUserRepository;
    private readonly PasswordUtil _passwordUtil;

    public AuthUserService(IAuthUserRepository authUserRepository, PasswordUtil passwordUtil)
    {
        _authUserRepository = authUserRepository;
        _passwordUtil = passwordUtil;
    }

    public async Task<ApiResponse<AuthUserResponse>> RegisterAsync(AuthUserRegister authUserRegister, CancellationToken cancellationToken)
    {
        try
        {
            if (!authUserRegister.Password1.Equals(authUserRegister.Password2))
                return new ApiResponse<AuthUserResponse>
                {
                    IsSuccess = false,
                    StatusCode = 400,
                    Message = "Las contraseñas no Coinciden.",
                };

            var authUser = await _authUserRepository.GetUserByEmailAsync(authUserRegister.Email, cancellationToken);

            if (authUser != null)
                return new ApiResponse<AuthUserResponse>
                {
                    IsSuccess = false,
                    StatusCode = 400,
                    Message = "Ya estas registrado.",
                };

            var (hash, salt) = _passwordUtil.HashPassword(authUserRegister.Password1);
            var authUserResponse = new AuthUserResponse
            {
                User_Id = Guid.NewGuid()
            };

            var newAuthUser = await _authUserRepository.CreateUserAsync(
                new AuthUser
                {
                    User_Id = authUserResponse.User_Id,
                    Email = authUserRegister.Email,
                    HashLogin = hash,
                    SaltLogin = salt,
                }, 
                cancellationToken);

            return new ApiResponse<AuthUserResponse>
            {
                IsSuccess = newAuthUser.IsSuccess,
                StatusCode = newAuthUser.StatusCode,
                Message = newAuthUser.Message,
                Data = authUserResponse
            };
        }
        catch (Exception ex)
        {
            return new ApiResponse<AuthUserResponse>
            {
                IsSuccess = false,
                StatusCode = 500,
                Message = ex.Message,
            };
        }

    }
}
