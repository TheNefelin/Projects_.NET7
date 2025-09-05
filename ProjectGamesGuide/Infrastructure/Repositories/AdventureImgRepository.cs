using Core.Data;
using Dapper;
using ProjectGamesGuide.Domain.Entities;
using ProjectGamesGuide.Domain.Interfaces;

namespace ProjectGamesGuide.Infrastructure.Repositories;

public class AdventureImgRepository : IRepositoryBase<AdventureImg>
{
    private readonly IDapperContext _dapper;

    public AdventureImgRepository(IDapperContext dapper)
    {
        _dapper = dapper;
    }

    public async Task<IEnumerable<AdventureImg>> GetAllAsync(CancellationToken cancellationToken)
    {
        var commandDefinition = new CommandDefinition(
            commandText: "SELECT Id, ImgUrl, Sort, Id_Adventure FROM GG_AdventuresImg",
            cancellationToken: cancellationToken
        );

        using var connection = _dapper.CreateConnection();
        return await connection.QueryAsync<AdventureImg>(commandDefinition);
    }
}
