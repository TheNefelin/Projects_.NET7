using Core.Data;

namespace ProjectPortfolio.Infrastructure.Repositories;

public class TechnologyRepository
{
    private readonly IDapperContext _dapper;

    public TechnologyRepository(IDapperContext dapper)
    {
        _dapper = dapper;
    }
}
