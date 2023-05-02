using Utils;
using System;
using Json2CSharp;
using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public static class DynamicWeatherController
{
    private static string currenCity;

    private static GameplayData data;

    private const string Weather = "weather", Astronomy = "astronomy";

    public static void Fetch(MonoBehaviour mono, City city, List<Api> api, Action<GameplayData> completed)
    {
        var weather = api.FirstOrDefault(a => a.name.Equals(Weather));

        var astronomy = api.FirstOrDefault(a => a.name.Equals(Astronomy));

        if (weather == null || astronomy == null)
        {
            return;
        }

        if (currenCity == city.name)
        {
            completed?.Invoke(data);

            return;
        }

        currenCity = city.name;

        mono.StartCoroutine(UNet.Fetch($"{weather.url}/data/2.5/weather?q={city.name},{city.country}&APPID={weather.key}&units=metric", (WeatherData wd) =>
        {
            mono.StartCoroutine(UNet.Fetch($"{astronomy.url}/astronomy?apiKey={astronomy.key}&lat={wd.coord.lat}&long={wd.coord.lon}", (AstronomyData ad) =>
            {
                data = new GameplayData(city.name,
                                        city.country,
                                        ad.sun_altitude,
                                        ad.sun_azimuth,
                                        ad.sun_distance,
                                        ad.moon_altitude,
                                        ad.moon_azimuth,
                                        ad.moon_distance,
                                        wd.coord.lat,
                                        wd.coord.lon,
                                        wd.main?.temp ?? 0,
                                        wd.main?.humidity ?? 0,
                                        wd.wind?.speed ?? 0,
                                        wd.wind?.deg ?? 0,
                                        wd.clouds?.all ?? 0,
                                        wd.rain?.oneHour ?? 0,
                                        wd.Weather.description,
                                        DateTime.UtcNow.AddHours(city.gmt));

                completed?.Invoke(data);
            }));
        }));
    }
}