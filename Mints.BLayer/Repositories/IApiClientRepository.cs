using Mints.DLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BApiClient = Mints.BLayer.Models.ApiClient.ApiClient;
using DApiClient = Mints.DLayer.Models.ApiClient;

namespace Mints.BLayer.Repositories
{
    public interface IApiClientRepository : IGenericRepository<DApiClient>
    {
        Task<bool> Exists(string apiKey);

        Task<BApiClient> FindByApiKeyAsync(string apiKey);

    }
}
