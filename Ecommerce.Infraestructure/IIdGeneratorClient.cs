using Refit;

namespace Ecommerce.Infraestructure
{
    public interface IIdGeneratorClient
    {
        [Get("id/bulk-create")]
        Task<IEnumerable<long>> Create(int count);
        [Get("id/new-id")]
        Task<long> CreateAsync();
    }
}