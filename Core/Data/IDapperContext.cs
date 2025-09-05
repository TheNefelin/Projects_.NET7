using System.Data;

namespace Core.Data;

public interface IDapperContext
{
    IDbConnection CreateConnection();
}
