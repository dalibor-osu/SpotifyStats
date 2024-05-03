using InfluxDB.Client.Core;
using Newtonsoft.Json;
using static SpotifyStats.Constants.Influx;

namespace SpotifyStats;

[Measurement(SPOTIFY_STATS)]
public class StatsEntry
{
    [JsonProperty("ts")]
    [Column(TIME_STAMP, IsTimestamp = true)]
    public DateTimeOffset TimeStamp { get; set; }

    [JsonProperty("username")]
    [Column(USERNAME, IsTag = true)]
    public string Username { get; set; } = Constants.UNKNOWN;
    
    [JsonProperty("platform")]
    [Column(PLATFORM)]
    public string? Platform { get; set; }
        
    [JsonProperty("ms_played")]
    [Column(MILLISECONDS_PLAYED)]
    public int MillisecondsPlayed { get; set; }
        
    [JsonProperty("conn_country")]
    [Column(CONNECTION_COUNTRY)]
    public string ConnectionCountry { get; set; } = string.Empty;
        
    [JsonProperty("ip_addr_decrypted")]
    [Column(CONNECTION_IP_ADDRESS)]
    public string ConnectionIpAddress { get; set; } = string.Empty;
        
    [JsonProperty("user_agent_decrypted")]
    [Column(USER_AGENT_DECRYPTED)]
    public string UserAgentDecrypted { get; set; } = string.Empty;
        
    [JsonProperty("master_metadata_track_name")]
    [Column(TRACK_NAME, IsTag = true)]
    public string? TrackName { get; set; }
        
    [JsonProperty("master_metadata_album_artist_name")]
    [Column(ALBUM_ARTIST_NAME, IsTag = true)]
    public string? AlbumArtistName { get; set; }
        
    [JsonProperty("master_metadata_album_album_name")]
    [Column(ALBUM_NAME, IsTag = true)]
    public string? AlbumName { get; set; }
        
    [JsonProperty("spotify_track_uri")]
    [Column(SPOTIFY_TRACK_URI)]
    public string? SpotifyTrackUri { get; set; }
        
    [JsonProperty("episode_name")]
    [Column(EPISODE_NAME)]
    public string? EpisodeName { get; set; }
        
    [JsonProperty("episode_show_name")]
    [Column(EPISODE_SHOW_NAME)]
    public string? EpisodeShowName { get; set; }
        
    [JsonProperty("spotify_episode_uri")]
    [Column(SPOTIFY_EPISODE_URI)]
    public string? SpotifyEpisodeUri { get; set; }
        
    [JsonProperty("reason_start")]
    [Column(REASON_START)]
    public string ReasonStart { get; set; } = string.Empty;
        
    [JsonProperty("reason_end")]
    [Column(REASON_END)]
    public string ReasonEnd { get; set; } = string.Empty;
        
    [JsonProperty("shuffle")]
    [Column(SHUFFLE)]
    public bool? Shuffle { get; set; }
        
    [JsonProperty("skipped")]
    [Column(SKIPPED)]
    public bool? Skipped { get; set; }
        
    [JsonProperty("offline")]
    [Column(OFFLINE)]
    public bool? Offline { get; set; }
    
    [JsonProperty("offline_timestamp")]
    [Column(OFFLINE_TIME_STAMP)]
    public DateTimeOffset? OfflineTimeStamp { get; set; }
        
    [JsonProperty("incognito_mode")]
    [Column(INCOGNITO_MODE)]
    public bool IncognitoMode { get; set; }
}