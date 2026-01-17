using Microsoft.PowerPlatform.Dataverse.Client;
using System;

namespace PrimeiroConsole
{
    class Connection
    {
        public ServiceClient ConnectionClientCredential()
        {
            string conn = $@"Url={Environment.GetEnvironmentVariable("DV_URL")};
                AuthType=ClientSecret;
                TenantId={Environment.GetEnvironmentVariable("DV_TENANT_ID")};
                ClientId={Environment.GetEnvironmentVariable("DV_CLIENT_ID")};
                ClientSecret={Environment.GetEnvironmentVariable("DV_CLIENT_SECRET")};
                RequireNewInstance=True";

            return new ServiceClient(conn);
        }
    }
}
