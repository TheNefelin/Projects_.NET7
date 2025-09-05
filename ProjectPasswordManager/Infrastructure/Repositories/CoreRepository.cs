using Core.Data;
using Dapper;
using System.Data;
using System.Threading;

namespace ProjectPasswordManager.Infrastructure.Repositories;

public class CoreRepository
{
    private readonly IDapperContext _dapper;

    public CoreRepository(IDapperContext dapper)
    {
        _dapper = dapper;
    }
    public async Task<AuthUser?> GetUser(string IdUser, string SqlToken, CancellationToken cancellationToken)
    {
        var commandDefinition = new CommandDefinition(
            commandText: "SELECT a.Id AS IdUser, a.Email, a.HashLogin, a.SaltLogin, a.HashPM, a.SaltPM, a.SqlToken, b.Name AS Role FROM Auth_Users a INNER JOIN Auth_Profiles b ON a.IdProfile = b.Id WHERE a.Id = @IdUser AND a.SqlToken = @SqlToken",
            parameters: new { IdUser, SqlToken },
            cancellationToken: cancellationToken
        );

        using var connection = _dapper.CreateConnection();
        return await connection.QueryFirstOrDefaultAsync<AuthUser>(commandDefinition);
    }
}
