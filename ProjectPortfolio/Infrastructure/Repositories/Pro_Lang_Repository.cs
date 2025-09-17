using Core.Data;

namespace ProjectPortfolio.Infrastructure.Repositories;

public class Pro_Lang_Repository
{
    private readonly IDapperContext _dapper;

    public Pro_Lang_Repository(IDapperContext dapper)
    {
        _dapper = dapper;
    }
}
