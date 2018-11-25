using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mints.Models.ApiModels
{
    public class TokenRequestModel : BaseModel
    {
        public TokenRequestModel()
        {
            Data = new Dictionary<string, string>();
        }
        public DateTime Expires { get; set; }

        public string Token { get; set; }

        public IDictionary<string, string> Data { get; set; }
    }
}
