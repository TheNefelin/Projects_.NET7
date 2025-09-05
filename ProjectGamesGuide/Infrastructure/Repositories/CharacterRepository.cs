using Core.Data;
using Dapper;
using ProjectGamesGuide.Domain.Entities;
using ProjectGamesGuide.Domain.Interfaces;

namespace ProjectGamesGuide.Infrastructure.Repositories;

public class CharacterRepository : IRepositoryBase<Character>
{
    private readonly IDapperContext _dapper;

    public CharacterRepository(IDapperContext dapper) 
    {
        _dapper = dapper;
    }

    public async Task<IEnumerable<Character>> GetAllAsync(CancellationToken cancellationToken)
    {
        var commandDefinition = new CommandDefinition(
            commandText: "SELECT Id, Name, Description, ImgUrl, Id_Game FROM GG_Characters",
            cancellationToken: cancellationToken
        );

        using var connection = _dapper.CreateConnection();
        return await connection.QueryAsync<Character>(commandDefinition);
    }
}
