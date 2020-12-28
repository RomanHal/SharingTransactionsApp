using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace IdServer
{
    internal class Clients
    {
        public static IEnumerable<Client> Get()
        {
            return new List<Client>
        {
            new Client
            {
                ClientId = "sharingClient",
                ClientName = "Example Client Application",
                ClientSecrets = new List<Secret> {new Secret("SuperSecretPassword".Sha256())},
    
                AllowedGrantTypes =GrantTypes.Code,
                RedirectUris = new List<string>{"https://localhost:5003/signin-oidc"},
                BackChannelLogoutUri="https://localhost:5003/logout",
                AllowedScopes = new List<string>
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    IdentityServerConstants.StandardScopes.Email,
                    "role",
                    "api1.read",
                    "api1.write"
                },
                RequireConsent=false,
                RequirePkce = true,
                AllowPlainTextPkce = false
            },

        };
        }
    }


}
