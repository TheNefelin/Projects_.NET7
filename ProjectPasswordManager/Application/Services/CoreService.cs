using Core;
using ProjectPasswordManager.Application.Interfaces;
using ProjectPasswordManager.Domain.Entities;
using ProjectPasswordManager.Domain.Interfaces;

namespace ProjectPasswordManager.Application.Services;

public class CoreService : ICoreService
{
    private readonly ICoreRepository _repository;

    public CoreService(ICoreRepository repository)
    {
        _repository = repository;
    }

    public async Task<ApiResponse<IEnumerable<CoreData>>> GetAllAsync(Guid idUser, CancellationToken cancellationToken)
    {
        try
        {
            var data = await _repository.GetAllAsync(idUser, cancellationToken);
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
            await _repository.DeleteAsync(coreDataDelete, cancellationToken);
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
