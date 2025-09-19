using Core.Data;
using Dapper;
using ProjectPortfolio.Domain.Entities;
using ProjectPortfolio.Domain.Interfaces;

namespace ProjectPortfolio.Infrastructure.Repositories;

public class LanguageRepository : IRepositoryPortfolioBase<Language>
{
    private readonly IDapperContext _dapper;

    public LanguageRepository(IDapperContext dapper)
    {
        _dapper = dapper;
    }

    public async Task<IEnumerable<Language>> GetAllAsync(CancellationToken cancellationToken)
    {
        var commandDefinition = new CommandDefinition(
            commandText: "SELECT a.Id, a.Name, a.ImgUrl, b.Id_Project FROM PF_Languages a INNER JOIN PF_Pro_Lang b ON a.Id = b.Id_Language",
            cancellationToken: cancellationToken
        );

        using var connection = _dapper.CreateConnection();
        return await connection.QueryAsync<Language>(commandDefinition);
    }
}
