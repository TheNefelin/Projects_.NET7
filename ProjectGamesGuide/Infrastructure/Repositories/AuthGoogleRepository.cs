using Core.Data;
using Dapper;
using ProjectGamesGuide.Application.DTOs;
using ProjectGamesGuide.Domain.Interfaces;
using System.Data;

namespace ProjectGamesGuide.Infrastructure.Repositories;

public class AuthGoogleRepository : IAuthGoogleRepository
{
    private readonly IDapperContext _dapper;

    public AuthGoogleRepository(IDapperContext dapper)
    {
        _dapper = dapper;
    }

    public async Task<LoggedGoogle> LoginGoogleAsync(LoginGoogle login, CancellationToken cancellationToken)
    {
        var commandDefinition = new CommandDefinition(
            commandType: CommandType.StoredProcedure,
             commandText: "GG_Login",
             parameters: new { login.Email, login.Sub, login.Jti },
             transaction: default,
             cancellationToken: cancellationToken
         );

        using var connection = _dapper.CreateConnection();
        return await connection.QueryFirstAsync<LoggedGoogle>(commandDefinition);
    }
}
