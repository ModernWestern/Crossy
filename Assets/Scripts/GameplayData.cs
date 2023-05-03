using System;
using Json2CSharp;
using ModernWestern;

public class GameplayData
{
    public readonly LocationData Location;

    public readonly bool? IsDay;

    public GameplayData(string city,
                        string country,
                        float sunAltitude,
                        float sunAzimuth,
                        float sunDistance,
                        float moonAltitude,
                        float moonAzimuth,
                        float moonDistance,
                        float lat,
                        float lon,
                        float temp,
                        float humidity,
                        float windSpeed,
                        float windDeg,
                        float clouds,
                        float rain,
                        string weatherDescription,
                        DateTime time,
                        bool? isDay)
    {
        Location.City = city;
        Location.Country = country;
        Location.Sun.Altitude = sunAltitude;
        Location.Sun.Azimuth = sunAzimuth;
        Location.Sun.Distance = sunDistance;
        Location.Moon.Altitude = moonAltitude;
        Location.Moon.Azimuth = moonAzimuth;
        Location.Moon.Distance = moonDistance;
        Location.Latitude = lat;
        Location.Longitude = lon;
        Location.Temp = temp;
        Location.Humidity = humidity;
        Location.WindSpeed = windSpeed;
        Location.WindAngle = windDeg;
        Location.Clouds = clouds;
        Location.Rain = rain;
        Location.WeatherDescription = weatherDescription.ToEnum<Weather.Description>();
        Location.Time = time;
        IsDay = isDay;
    }

    public override string ToString()
    {
        return Location.ToString();
    }
}

public struct LocationData
{
    public float Latitude, Longitude, Temp, Humidity, WindSpeed, WindAngle, Clouds, Rain;

    public Weather.Description WeatherDescription;

    public Astro Sun, Moon;

    public string City, Country;

    public DateTime Time;

    public string Name => $"{City.ToFirstUpper()},{Country.ToUpper()}";

    public override string ToString()
    {
        return $"<color=yellow>[{Name}]</color> Temp: {Temp}, Wind speed: {WindSpeed}, Wind angle: {WindAngle}, Clouds: {Clouds}, Rain: {Rain}, Weather Description: {WeatherDescription}";
    }
}

public struct Astro
{
    public float Altitude, Azimuth, Distance;

    public Astro(float altitude, float azimuth, float distance)
    {
        Altitude = altitude;

        Azimuth = azimuth;

        Distance = distance;
    }
}