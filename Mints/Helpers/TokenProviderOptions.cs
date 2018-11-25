using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mints.Helpers
{
    public class TokenProviderOptions
    {

        public string SecretKey { get; set; }

        public string TokenPath { get; set; } = "api/v1/auth/token";

        public string Issuer { get; set; }

        public string Audience { get; set; }

        public DateTime Expiration { get; set; } = DateTime.Now.AddDays(2);

        public SigningCredentials SigningCredentials { get; set; }

        public bool ValidateIssuerSigningKey { get; set; }

        public bool ValidateIssuer { get; set; }

        public bool ValidateLifetime { get; set; }

        public bool ValidateAudience { get; set; }

        public bool SaveToken { get; set; }

        public bool RequireHttpsMetadata { get; set; }

        public TimeSpan ClockSkew { get; set; } = TimeSpan.Zero;

    }
}
