# SpotifyStats
Simple .NET tool that takes data exported from Spotify and uploads them to [InfluxDB](https://www.influxdata.com/), which then allows complex querying and graph plotting.

## Usage
This app expects a JSON config file in the same directory as the executable. This file must be called `config.json` and it must contain these values:
```json
{
  "token": "<API TOKEN>",
  "url": "<INFLUXDB URL>",
  "organization": "<INFLUXDB ORGANIZATION>",
  "bucket": "<INFLUXDB BUCKET>"
}
```

Next up, you'll have to setup an InfluxDB instance. Since you can provide an URL, it's up to you if you're gonna host it yourself or use a cloud service.

After setting up your InfluxDB instance and running it, you can start importing your data. If you don't know how to download spotify history, you can use this [article](https://support.stats.fm/docs/import/spotify-import/). Simply build the app with `dotnet build` and then go to the output directory. You have 2 options now:
- Run the app with 2 arguments
  - First argument being the name of a user you're about to import
  - Second argument being a path to directory with spotify data (this directory should contain files named `Streaming_History_Audio_*.json`)
  - Example: `./SpotifyStats dalibor /home/dalibor/spotify_data/`
- Run the app without any arguments
  - You will then be asked for these 2 things

### Sample setup
I host InfluxDB by myself inside a docker container and I use a `docker-compose` file for that since I find it more comfortable to maintain. It looks like this in my case:
```yaml
version: "3.8"

services:
  database-influxdb:
    image: influxdb
    environment:
      - DOCKER_INFLUXDB_INIT_ORG=spotifyStats
      - DOCKER_INFLUXDB_INIT_USERNAME=admin
      - DOCKER_INFLUXDB_INIT_PASSWORD=<password>
      - DOCKER_INFLUXDB_INIT_BUCKET=stats
      - DOCKER_INFLUXDB_INIT_MODE=setup
      - DOCKER_INFLUXDB_INIT_ADMIN_TOKEN=<token>
    volumes:
      - influx_data:/var/lib/influxdb2:rw
    ports:
      - "8086:8086"

volumes:
  influx_data:
```

This setup does everything you need to start using InfluxDB with this tool. `config.json` would in this case look like this:
```json
{
  "token": "<token>",
  "url": "http://localhost:8086",
  "organization": "spotifyStats",
  "bucket": "stats"
}
```

I always run this tool from the same host as the InfluxDB instance, so I can use `localhost`. Note that password and token can be any string value.

## Sample Graphs
![image](https://github.com/dalibor-osu/SpotifyStats/assets/77931392/f9757848-d01d-40b6-8df3-e1cce3c914e4)
![image](https://github.com/dalibor-osu/SpotifyStats/assets/77931392/62b26078-7af1-41d4-8925-47b62745e22d)
