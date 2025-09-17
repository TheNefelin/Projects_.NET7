using Core.Data;

namespace ProjectPortfolio.Infrastructure.Repositories;

public class ProjectRepository
{
    private readonly IDapperContext _dapper;
    
    public ProjectRepository(IDapperContext dapper)
    {
        _dapper = dapper;
    }
}
