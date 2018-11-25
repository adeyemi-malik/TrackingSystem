using Mints.BLayer.Models.ApiClient;
using System;
using System.Collections.Generic;
using System.Text;
using DApiClient = Mints.DLayer.Models.ApiClient;

namespace Mints.BLayer.Helpers
{
    public static class ApiClientHelper
    {
        public static ApiClient ToApiClient(this DApiClient dApiClient) => new ApiClient
        {
            Id = dApiClient.Id,
            ApiKeyHash = dApiClient.ApiKeyHash,
            ClientName = dApiClient.ClientName,
            CallBackUrl = dApiClient.CallBackUrl
        };


        public static DApiClient ToDbCardPin(this ApiClient apiClient) => new DApiClient
        {
            Id = apiClient.Id,
            ClientName = apiClient.ClientName,
            ApiKeyHash = apiClient.ApiKeyHash,
            CallBackUrl = apiClient.CallBackUrl
        };
    }
}
