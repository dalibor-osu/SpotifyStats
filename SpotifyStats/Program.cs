﻿using InfluxDB.Client;
using InfluxDB.Client.Api.Domain;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SpotifyStats;
using File = System.IO.File;
using static PrettyLogSharp.PrettyLogger;

if (!File.Exists("./config.json"))
{
    Log("Config file \"config.json\" not found.", LogLevel.Error);
    return;
}

var settings = JsonConvert.DeserializeObject<Settings>(File.ReadAllText("./config.json"));
if (settings == null)
{
    Log("Failed to deserialize config file", LogLevel.Error);
    return;
}

Log("Who are you adding (leave empty to keep spotify username)?");
string username = Console.ReadLine() ?? string.Empty;
bool customName = username != string.Empty;

Log("Path to files directory:");
string path = Console.ReadLine() ?? string.Empty;

string[] files = Directory.GetFiles(path, "Streaming_History_Audio_*.json");

if (files.Length < 1)
{
    Log("No files found in: " + path, LogLevel.Error);
    return;
}

using var client = new InfluxDBClient(settings.Url, settings.Token).GetWriteApi();

foreach (string file in files)
{
    Log($"Processing file: {Path.GetFileName(file)}");
    var entries = JArray.Parse(File.ReadAllText(file));
    
    Log($"Found {entries.Count} entries");
    var entryList = new List<StatsEntry>();
    foreach (var entry in entries)
    {
        var statsEntry = JsonConvert.DeserializeObject<StatsEntry>(entry.ToString(), new SpotifyStatsConverter());
        if (statsEntry == null)
        {
            continue;
        }

        if (customName)
        {
            statsEntry.Username = username;
        }
        
        entryList.Add(statsEntry);
    }
    
    Log($"Writing {entries.Count} entries");
    client.WriteMeasurements(entryList, WritePrecision.Ns, settings.Bucket, settings.Organization);
}

Log("done");