using Core.Data;
using Dapper;
using ProjectPasswordManager.Domain.Entities;
using ProjectPasswordManager.Domain.Interfaces;

namespace ProjectPasswordManager.Infrastructure.Repositories;

public class CoreDataRepository : ICoreDataRepository
{
    private readonly IDapperContext _dapper;

    public CoreDataRepository(IDapperContext dapper)
    {
        _dapper = dapper;
    }

    public async Task<IEnumerable<CoreData>> GetAllAsync(CoreData coreData, CancellationToken cancellationToken)
    {
        var commandDefinition = new CommandDefinition(
            cancellationToken: cancellationToken,
            commandText: "SELECT Data_Id, Data01, Data02, Data03, User_Id FROM PM_Core WHERE User_Id = @User_Id",
            parameters: new {
                coreData.User_Id
            }
        );

        using var connection = _dapper.CreateConnection();
        return await connection.QueryAsync<CoreData>(commandDefinition);
    }

    public async Task<CoreData> InsertAsync(CoreData coreData, CancellationToken cancellationToken)
    {
        var commandDefinition = new CommandDefinition(
            cancellationToken: cancellationToken,
            commandText: "INSERT INTO PM_Core (Data01, Data02, Data03, User_Id) OUTPUT inserted.Id VALUES (@Data01, @Data02, @Data03, @User_Id)",
            parameters: new { 
                coreData.Data01, 
                coreData.Data02, 
                coreData.Data03, 
                coreData.User_Id
            }
        );

        using var connection = _dapper.CreateConnection();
        var id = await connection.QueryAsync<int>(commandDefinition);

        coreData.Data_Id = id.First();
        return coreData;
    }

    public async Task<CoreData> UpdateAsync(CoreData coreData, CancellationToken cancellationToken)
    {
        var commandDefinition = new CommandDefinition(
            cancellationToken: cancellationToken,
            commandText: "UPDATE PM_Core SET Data01 = @Data01, Data02 = @Data02, Data03 = @Data03 WHERE Data_Id = @Data_Id AND User_Id = @User_Id",
            parameters: new
            {
                coreData.Data_Id,
                coreData.Data01,
                coreData.Data02,
                coreData.Data03,
                coreData.User_Id
            }
        );

        using var connection = _dapper.CreateConnection();
        await connection.QueryAsync(commandDefinition);

        return coreData;
    }

    public async Task DeleteAsync(CoreData coreData, CancellationToken cancellationToken)
    {
        var commandDefinition = new CommandDefinition(
            cancellationToken: cancellationToken,
            commandText: "DELETE FROM PM_Core WHERE Data_Id = @Data_Id AND User_Id = @User_Id",
            parameters: new
            {
                coreData.Data_Id,
                coreData.User_Id
            }
        );

        using var connection = _dapper.CreateConnection();
        await connection.QueryAsync(commandDefinition);
    }
}
