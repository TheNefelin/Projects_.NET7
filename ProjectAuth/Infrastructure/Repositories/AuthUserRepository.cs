using Core.Data;
using Dapper;
using Microsoft.Win32;
using ProjectAuth.Domain.Entities;
using ProjectAuth.Infrastructure.Services;
using System.Data;
using System.Threading;

namespace ProjectAuth.Infrastructure.Repositories;

public class AuthUserRepository
{
    private readonly IDapperContext _dapper;
    private readonly PasswordUtil _passwordUtil;
    private readonly JwtTokenUtil _jwtUtil;

    public AuthUserRepository(IDapperContext dapper, PasswordUtil passwordUtil, JwtTokenUtil jwtUtil)
    {
        _dapper = dapper;
        _passwordUtil = passwordUtil;
        _jwtUtil = jwtUtil;
    }

    public async Task<int> CreateUserAsync(AuthUser authUser, CancellationToken cancellationToken)
    {
        var commandDefinition = new CommandDefinition(
            commandType: CommandType.StoredProcedure,
            commandText: "Auth_Register",
            parameters: new
            {
                authUser.IdUser,
                authUser.Email,
                HashLogin = authUser.HashPM,
                SaltLogin = authUser.SaltPM
            },
            transaction: default,
            cancellationToken: cancellationToken
        );

        using var connection = _dapper.CreateConnection();
        return await connection.QueryAsync<int>(commandDefinition);
    }
}
