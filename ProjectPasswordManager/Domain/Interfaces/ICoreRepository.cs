using ProjectPasswordManager.Application.DTOs;
using ProjectPasswordManager.Domain.Entities;

namespace ProjectPasswordManager.Domain.Interfaces;

public interface ICoreRepository
{
    Task<IEnumerable<CoreData>> GetAllAsync(Guid idUser, CancellationToken cancellationToken);
    Task<CoreData> InsertAsync(CoreData coreData, CancellationToken cancellationToken);
    Task<CoreData> UpdateAsync(CoreData coreData, CancellationToken cancellationToken);
    Task DeleteAsync(CoreDataDelete coreDataDelete, CancellationToken cancellationToken);
}
