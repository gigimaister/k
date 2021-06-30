using Refit;
using System.Threading.Tasks;

namespace SfGrid_Android
{
    public interface RefitApi
    {
        [Get("/{symbol}/")]
        Task<string> GetCompanyAsync(string symbol);
    }
}