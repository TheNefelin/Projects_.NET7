using ProjectPasswordManager.Domain.Interfaces;

namespace ProjectPasswordManager.Application.Services;

public class CoreUserService
{
    private readonly ICoreUserRepository _coreUserRepository;

    public CoreUserService(ICoreUserRepository coreUserRepository)
    {
        _coreUserRepository = coreUserRepository;
    }

    public async Task RegisterCoreUserPasswordAsync(CancellationToken cancellationToken)
    {

    }
}
