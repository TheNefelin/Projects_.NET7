using Core.Data;

namespace ProjectPortfolio.Infrastructure.Repositories;

public class UrlRepository
{
    private readonly IDapperContext _dapper;

    public UrlRepository(IDapperContext dapper)
    {
        _dapper = dapper;
    }
}
