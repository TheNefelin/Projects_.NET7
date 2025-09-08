using Core.Data;
using Dapper;
using ProjectPasswordManager.Domain.Entities;
using ProjectPasswordManager.Domain.Interfaces;

namespace ProjectPasswordManager.Infrastructure.Repositories;

public class CoreRepository : ICoreRepository
{
    private readonly IDapperContext _dapper;

    public CoreRepository(IDapperContext dapper)
    {
        _dapper = dapper;
    }

    public async Task<IEnumerable<CoreData>> GetAllAsync(Guid idUser, CancellationToken cancellationToken)
    {
        var commandDefinition = new CommandDefinition(
            cancellationToken: cancellationToken,
            commandText: "SELECT Id, Data01, Data02, Data03, IdUser FROM PM_Core WHERE IdUser = @IdUser",
            parameters: new { 
                IdUser = idUser
            }
        );

        using var connection = _dapper.CreateConnection();
        return await connection.QueryAsync<CoreData>(commandDefinition);
    }

    public async Task<CoreData> InsertAsync(CoreData coreData, CancellationToken cancellationToken)
    {
        var commandDefinition = new CommandDefinition(
            cancellationToken: cancellationToken,
            commandText: "INSERT INTO PM_Core (Data01, Data02, Data03, IdUser) OUTPUT inserted.Id VALUES (@Data01, @Data02, @Data03, @IdUser)",
            parameters: new { 
                coreData.Data01, 
                coreData.Data02, 
                coreData.Data03, 
                coreData.IdUser
            }
        );

        using var connection = _dapper.CreateConnection();
        var id = await connection.QueryAsync<int>(commandDefinition);

        coreData.Id = id.First();
        return coreData;
    }

    public async Task<CoreData> UpdateAsync(CoreData coreData, CancellationToken cancellationToken)
    {
        var commandDefinition = new CommandDefinition(
            cancellationToken: cancellationToken,
            commandText: "UPDATE PM_Core SET Data01 = @Data01, Data02 = @Data02, Data03 = @Data03 WHERE Id = @Id AND IdUser = @IdUser",
            parameters: new
            {
                coreData.Id,
                coreData.Data01,
                coreData.Data02,
                coreData.Data03,
                coreData.IdUser
            }
        );

        using var connection = _dapper.CreateConnection();
        await connection.QueryAsync(commandDefinition);

        return coreData;
    }

    public async Task DeleteAsync(int id, Guid idUser, CancellationToken cancellationToken)
    {
        var commandDefinition = new CommandDefinition(
            cancellationToken: cancellationToken,
            commandText: "DELETE FROM PM_Core WHERE Id = @Id AND IdUser = @IdUser",
            parameters: new
            {
                Id = id,
                IdUser = idUser
            }
        );

        using var connection = _dapper.CreateConnection();
        await connection.QueryAsync(commandDefinition);
    }
}
