using Newtonsoft.Json;

namespace NetHttpClient.Test
{
    public class UserDto
    {
        [JsonProperty("username")]
        public string Username { get; set; }
        [JsonProperty("password")]
        public string Password { get; set; }
        [JsonProperty("fullName")]
        public string FullName { get; set; }

        [JsonProperty("roleId")]
        public long RoleId { get; set; }
    }
}
