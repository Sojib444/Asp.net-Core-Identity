

using Newtonsoft.Json;

namespace Identity.Authorization
{
    public class JwtToken
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("expiryTime")]
        public string ExpirayAt { get; set; }
    }
}
