using Core.Data;
using Dapper;
using ProjectGamesGuide.Domain.Entities;
using ProjectGamesGuide.Domain.Interfaces;
using System.Data;

namespace ProjectGamesGuide.Infrastructure.Repositories;

public class AdventureUserRepository : IRepositoryByUser<AdventureUser>
{
    private readonly IDapperContext _dapper;

    public AdventureUserRepository(IDapperContext dapper)
    {
        _dapper = dapper;
    }

    public async Task<IEnumerable<AdventureUser>> GetAllByUserIdAsync(Guid Id_User, CancellationToken cancellationToken)
    {
        var commandDefinition = new CommandDefinition(
            commandText: "SELECT Id_Adventure, Id_User, IsCheck FROM GG_AdventuresUser WHERE Id_User = @Id_User",
            parameters: new { Id_User },
            cancellationToken: cancellationToken
        );

        using var connection = _dapper.CreateConnection();
        return await connection.QueryAsync<AdventureUser>(commandDefinition);
    }

    public async Task<object> UpdateAsync(AdventureUser userData, CancellationToken cancellationToken)
    {
        var commandDefinition = new CommandDefinition(
               commandType: CommandType.StoredProcedure,
               commandText: "GG_AdventuresUser_Set",
               parameters: new { userData.Id_Adventure, userData.Id_User, userData.IsCheck },
               cancellationToken: cancellationToken
           );

        using var connection = _dapper.CreateConnection();
        return await connection.QueryAsync<object>(commandDefinition);
    }
}
