using Core.Data;

namespace ProjectPortfolio.Infrastructure.Repositories;

public class UrlGrpRepository
{
    private readonly IDapperContext _dapper;

    public UrlGrpRepository(IDapperContext dapper)
    {
        _dapper = dapper;
    }
}
