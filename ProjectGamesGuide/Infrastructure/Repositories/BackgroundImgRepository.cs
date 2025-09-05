using Core.Data;
using Dapper;
using ProjectGamesGuide.Domain.Entities;
using ProjectGamesGuide.Domain.Interfaces;

namespace ProjectGamesGuide.Infrastructure.Repositories;

public class BackgroundImgRepository : IRepositoryBase<BackgroundImg>
{
    private readonly IDapperContext _dapper;

    public BackgroundImgRepository(IDapperContext dapper)
    {
        _dapper = dapper;
    }

    public async Task<IEnumerable<BackgroundImg>> GetAllAsync(CancellationToken cancellationToken)
    {
        var commandDefinition = new CommandDefinition(
            commandText: "SELECT Id, ImgUrl, Id_Game FROM GG_BackgroundsImg",
            cancellationToken: cancellationToken
        );

        using var connection = _dapper.CreateConnection();
        return await connection.QueryAsync<BackgroundImg>(commandDefinition);
    }
}
