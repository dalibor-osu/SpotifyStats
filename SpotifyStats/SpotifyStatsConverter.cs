using Newtonsoft.Json;

namespace SpotifyStats;

public class SpotifyStatsConverter : JsonConverter<DateTimeOffset?>
{
    public override void WriteJson(JsonWriter writer, DateTimeOffset? value, JsonSerializer serializer)
    {
        if (!value.HasValue)
        {
            writer.WriteValue("null");
            return;
        }
        
        writer.WriteValue(Convert.ToInt64((value.Value - DateTimeOffset.FromUnixTimeSeconds(0)).TotalMilliseconds));
    }

    public override DateTimeOffset? ReadJson(JsonReader reader, Type objectType, DateTimeOffset? existingValue, bool hasExistingValue,
        JsonSerializer serializer)
    {
        object? value = reader.Value;
        if (value != null)
        {
            switch (value)
            {
                case DateTime dateTime:
                    return new DateTimeOffset(dateTime, TimeZoneInfo.Utc.BaseUtcOffset);
                case long timestamp:
                    if (timestamp < 1)
                    {
                        return null;
                    }
                    
                    return DateTimeOffset.FromUnixTimeMilliseconds(timestamp);
            }
        }

        if (reader.Path == "ts")
        {
            throw new SpotifyEntryException("ts can't be null");
        }

        return null;
    }
}