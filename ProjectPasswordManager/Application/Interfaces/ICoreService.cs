using Core;
using ProjectPasswordManager.Domain.Entities;

namespace ProjectPasswordManager.Application.Interfaces;

public interface ICoreService
{
    Task<ApiResponse<IEnumerable<CoreData>>> GetAllAsync(Guid idUser, CancellationToken cancellationToken);
    Task<ApiResponse<CoreData>> InsertAsync(CoreData coreData, CancellationToken cancellationToken);
    Task<ApiResponse<CoreData>> UpdateAsync(CoreData coreData, CancellationToken cancellationToken);
    Task<ApiResponse<object>> DeleteAsync(int id, Guid idUser, CancellationToken cancellationToken);
}
