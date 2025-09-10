using Core;
using ProjectPasswordManager.Application.DTOs;
using ProjectPasswordManager.Domain.Entities;

namespace ProjectPasswordManager.Application.Interfaces;

public interface ICoreDataService
{
    Task<ApiResponse<IEnumerable<CoreData>>> GetAllAsync(CoreUserRequest coreUser, CancellationToken cancellationToken);
    Task<ApiResponse<CoreData>> InsertAsync(CoreData coreData, CancellationToken cancellationToken);
    Task<ApiResponse<CoreData>> UpdateAsync(CoreData coreData, CancellationToken cancellationToken);
    Task<ApiResponse<object>> DeleteAsync(CoreDataDelete coreDataDelete, CancellationToken cancellationToken);
}
