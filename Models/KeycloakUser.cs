using Newtonsoft.Json;

namespace Models
{
    public class KeycloakUser
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("firstName")]
        public string Firstname { get; set; }

        [JsonProperty("lastName")]
        public string Lastname { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }



    }
}
