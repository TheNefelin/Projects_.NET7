using Core;
using ProjectPasswordManager.Application.DTOs;
using ProjectPasswordManager.Application.Interfaces;
using ProjectPasswordManager.Domain.Entities;
using ProjectPasswordManager.Domain.Interfaces;

namespace ProjectPasswordManager.Application.Services;

public class CoreDataService : ICoreDataService
{
    private readonly ICoreDataRepository _repository;
    private readonly ICoreUserRepository _coreUserRepository;

    public CoreDataService(ICoreDataRepository repository, ICoreUserRepository coreUserRepository)
    {
        _repository = repository;
        _coreUserRepository = coreUserRepository;
    }

    public async Task<ApiResponse<IEnumerable<CoreData>>> GetAllAsync(CoreUserRequest coreUserRequest, CancellationToken cancellationToken)
    {
        try
        {
            var user = new CoreUser
            {
                User_Id = coreUserRequest.User_Id,
                SqlToken = coreUserRequest.SqlToken
            };

            var coreUser = await _coreUserRepository.GetCoreUserAsync(user, cancellationToken);

            if (coreUser == null)
                return new ApiResponse<IEnumerable<CoreData>>
                {
                    IsSuccess = false,
                    StatusCode = 401,
                    Message = "Debes Iniciar Sesion."
                };

            var data = await _repository.GetAllAsync(user, cancellationToken);
            return new ApiResponse<IEnumerable<CoreData>>
            {
                IsSuccess = true,
                StatusCode = 200,
                Message = "Ok",
                Data = data
            };
        }
        catch (Exception ex)
        {
            return new ApiResponse<IEnumerable<CoreData>>
            {
                IsSuccess = false,
                StatusCode = 500,
                Message = ex.Message,
            };
        }
    }

    public async Task<ApiResponse<CoreData>> InsertAsync(CoreData coreData, CancellationToken cancellationToken)
    {
        try
        {
            var data = await _repository.InsertAsync(coreData, cancellationToken);
            return new ApiResponse<CoreData>
            {
                IsSuccess = true,
                StatusCode = 201,
                Message = "Created",
                Data = data
            };
        }
        catch (Exception ex)
        {
            return new ApiResponse<CoreData>
            {
                IsSuccess = false,
                StatusCode = 500,
                Message = ex.Message,
            };
        }
    }

    public async Task<ApiResponse<CoreData>> UpdateAsync(CoreData coreData, CancellationToken cancellationToken)
    {
        try
        {
            var data = await _repository.UpdateAsync(coreData, cancellationToken);
            return new ApiResponse<CoreData>
            {
                IsSuccess = true,
                StatusCode = 200,
                Message = "Ok",
                Data = data
            };
        }
        catch (Exception ex)
        {
            return new ApiResponse<CoreData>
            {
                IsSuccess = false,
                StatusCode = 500,
                Message = ex.Message,
            };
        }
    }

    public async Task<ApiResponse<object>> DeleteAsync(CoreDataDelete coreDataDelete, CancellationToken cancellationToken)
    {
        try
        {
            var coreData = new CoreData
            {
                Data_Id = coreDataDelete.Data_Id,
                User_Id = coreDataDelete.CoreUser.User_Id,
            };

            await _repository.DeleteAsync(coreData, cancellationToken);
            return new ApiResponse<object>
            {
                IsSuccess = true,
                StatusCode = 204,
                Message = "No Content"
            };
        }
        catch (Exception ex)
        {
            return new ApiResponse<object>
            {
                IsSuccess = false,
                StatusCode = 500,
                Message = ex.Message,
            };
        }
    }
}
