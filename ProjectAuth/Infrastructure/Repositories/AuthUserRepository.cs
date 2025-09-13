using Core;
using Core.Data;
using Dapper;
using ProjectAuth.Domain.Entities;
using ProjectAuth.Domain.Interfaces;
using System.Data;

namespace ProjectAuth.Infrastructure.Repositories;

public class AuthUserRepository : IAuthUserRepository
{
    private readonly IDapperContext _dapper;

    public AuthUserRepository(IDapperContext dapper)
    {
        _dapper = dapper;
    }

    public async Task<SqlResponse?> CreateUserAsync(AuthUser authUser, CancellationToken cancellationToken)
    {
        var commandDefinition = new CommandDefinition(
            commandType: CommandType.StoredProcedure,
            commandText: "Auth_Register",
            parameters: new
            {
                authUser.User_Id,
                authUser.Email,
                HashLogin = authUser.HashPM,
                SaltLogin = authUser.SaltPM
            },
            transaction: default,
            cancellationToken: cancellationToken
        );

        using var connection = _dapper.CreateConnection();
        return await connection.QueryFirstOrDefaultAsync<SqlResponse>(commandDefinition);
    }

    public async Task<AuthUser?> GetUserByEmailAsync(string email, CancellationToken cancellationToken)
    {
        var commandDefinition = new CommandDefinition(
            commandType: CommandType.StoredProcedure,
            commandText: "Auth_Login",
            parameters: new 
            { 
                Email = email 
            },
            transaction: default,
            cancellationToken: cancellationToken
        );
        using var connection = _dapper.CreateConnection();
        return await connection.QueryFirstOrDefaultAsync<AuthUser>(commandDefinition);
    }
}
