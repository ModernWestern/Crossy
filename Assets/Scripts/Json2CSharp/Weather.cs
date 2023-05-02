using System;
using Newtonsoft.Json;
using JetBrains.Annotations;
using System.Collections.Generic;

namespace Json2CSharp
{
    [Serializable]
    public class WeatherData
    {
        public Coord coord;
        [CanBeNull] public Main main;
        [CanBeNull] public Wind wind;
        [CanBeNull] public Rain rain;
        [CanBeNull] public Clouds clouds;
        [CanBeNull] public List<Weather> weather;

        public Weather Weather => weather?.Count > 0 ? weather[0] : null;
    }

    [Serializable]
    public class Clouds
    {
        public int all;
    }

    [Serializable]
    public class Coord
    {
        public float lon;
        public float lat;
    }

    [Serializable]
    public class Main
    {
        public float temp;
        public int humidity;
    }

    [Serializable]
    public class Rain
    {
        [JsonProperty("1h")] public float oneHour;
    }

    [Serializable]
    public class Weather
    {
        [JsonProperty("main")] public string description;

        public enum Description
        {
            Thunderstorm,
            Clear,
            Clouds,
            Haze,
            Rain,
            Fog
        }
    }

    [Serializable]
    public class Wind
    {
        public float speed;
        public int deg;
    }
}