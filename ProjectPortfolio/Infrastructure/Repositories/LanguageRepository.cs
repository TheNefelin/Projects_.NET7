using Core.Data;

namespace ProjectPortfolio.Infrastructure.Repositories;

public class LanguageRepository
{
    private readonly IDapperContext _dapper;

    public LanguageRepository(IDapperContext dapper)
    {
        _dapper = dapper;
    }
}
