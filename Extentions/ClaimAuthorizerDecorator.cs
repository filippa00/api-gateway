using Ocelot.Authorization;
using Ocelot.DownstreamRouteFinder.UrlMatcher;
using Ocelot.Responses;
using System.Security.Claims;

namespace Extentions
{
    public class ClaimAuthorizerDecorator : IClaimsAuthorizer
    {
        private readonly ClaimsAuthorizer _authoriser;

        public ClaimAuthorizerDecorator(ClaimsAuthorizer authoriser)
        {
            _authoriser = authoriser;
        }

        public Response<bool> Authorize(ClaimsPrincipal claimsPrincipal,
                                        Dictionary<string, string> routeClaimsRequirement,
                                        List<PlaceholderNameAndValue> urlPathPlaceholderNameAndValues)
        {
            var newRouteClaimsRequirement = new Dictionary<string, string>();
            foreach (var kvp in routeClaimsRequirement)
            {
                if (kvp.Key.StartsWith("http///"))
                {
                    var key = kvp.Key.Replace("http///", "http://");
                    newRouteClaimsRequirement.Add(key, kvp.Value);
                }
                else
                {
                    newRouteClaimsRequirement.Add(kvp.Key, kvp.Value);
                }
            }

            return _authoriser.Authorize(claimsPrincipal, newRouteClaimsRequirement, urlPathPlaceholderNameAndValues);
        }
    }

}


