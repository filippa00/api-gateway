using Microsoft.Win32.SafeHandles;
using Newtonsoft.Json;

namespace Models
{
    public class LoginToken
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; }

    }
}