using Newtonsoft.Json;

namespace Models
{
    public class LoginToken
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; }

        public string AdminId => "3747c27b-871a-482d-99c8-d7f3882bea33";

    }
}