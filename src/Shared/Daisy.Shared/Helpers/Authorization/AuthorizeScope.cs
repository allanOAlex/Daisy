using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daisy.Shared.Helpers.Authorization
{
    public class AuthorizeScope : AuthorizeAttribute
    {
        const string POLICY_PREFIX = "AllowedScope";

        public AuthorizeScope(params string[] scopes)
        {
            Scopes = scopes;
        }

        public string[] Scopes
        {
            get
            {
                var scopes = Policy.Substring(POLICY_PREFIX.Length);
                if (!string.IsNullOrWhiteSpace(scopes))
                {
                    return scopes.Split(":");
                }
                return Array.Empty<string>();
            }

            set
            {
                Policy = $"{POLICY_PREFIX}{string.Join(":", value)}";
            }
        }
    }
}
