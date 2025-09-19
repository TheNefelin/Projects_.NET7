using Core;

namespace ProjectPortfolio.Application.Interfaces;

public interface IServicePortfolioBase<T>
{
    Task<ApiResponse<IEnumerable<T>>> GetAllAsync(CancellationToken cancellationToken);
}
