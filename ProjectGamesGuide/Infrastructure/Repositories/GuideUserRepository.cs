using Core.Data;
using Dapper;
using ProjectGamesGuide.Domain.Entities;
using ProjectGamesGuide.Domain.Interfaces;

namespace ProjectGamesGuide.Infrastructure.Repositories;

public class GuideUserRepository : IRepositoryByUser<GuideUser>
{
    private readonly IDapperContext _dapper;

    public GuideUserRepository(IDapperContext dapper)
    {
        _dapper = dapper;
    }

    public async Task<IEnumerable<GuideUser>> GetAllByUserIdAsync(Guid Id_User, CancellationToken cancellationToken)
    {
        var commandDefinition = new CommandDefinition(
            commandText: "SELECT Id_Guide, Id_User, IsCheck FROM GG_GuidesUser WHERE Id_User = @Id_User",
            parameters: new { Id_User },
            cancellationToken: cancellationToken
        );

        using var connection = _dapper.CreateConnection();
        return await connection.QueryAsync<GuideUser>(commandDefinition);
    }
}
