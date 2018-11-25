using Mints.DLayer.Models.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mints.BLayer.Models.ApiClient
{
    public class ApiClient : IEntity
    {
        public int Id { get; set; }

        public string ClientName { get; set; }
       
        public string CallBackUrl { get; set; }
        
        public string ApiKeyHash { get; set; }
    }
}
