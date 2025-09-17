using Core.Data;

namespace ProjectPortfolio.Infrastructure.Repositories;

public class Pro_Tech_Repository
{
    private readonly IDapperContext _dapper;

    public Pro_Tech_Repository(IDapperContext dapper)
    {
        _dapper = dapper;
    }
}
