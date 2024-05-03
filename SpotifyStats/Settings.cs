using Newtonsoft.Json;

namespace SpotifyStats;

public class Settings
{
    [JsonProperty("token")]
    public string Token { get; set; }
    
    [JsonProperty("url")]
    public string Url { get; set; }
    
    [JsonProperty("organization")]
    public string Organization { get; set; }

    [JsonProperty("bucket")]
    public string Bucket { get; set; }
}