using Mints.BLayer.Helpers;
using Mints.BLayer.Utils;
using Mints.DLayer.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using BApiClient = Mints.BLayer.Models.ApiClient.ApiClient;
using DApiClient = Mints.DLayer.Models.ApiClient;

namespace Mints.BLayer.Repositories
{
    public class ApiClientRepository : GenericRepository<DApiClient>, IApiClientRepository
    {
        public ApiClientRepository(Context dbcontext)
        {
            _dbContext = dbcontext;
        }

        public async Task<bool> Exists(string apiKey)
        {
            var hash = Util.GenerateSHA512String(apiKey);
            var items = await Query().ToListAsync();
             var exist = items.Any(p => p.ApiKeyHash == hash);

            return exist;
        }

        public async Task<BApiClient> FindByApiKeyAsync(string apiKey)
        {
            var hash = Util.GenerateSHA512String(apiKey);
            var apiClient = await Query()
                .SingleOrDefaultAsync(p => p.ApiKeyHash == hash);

            if (apiClient == null)
            {
                throw new System.NullReferenceException("The  api client does not exist for a valid client");
            }
            return apiClient.ToApiClient();
        }
    }
}
