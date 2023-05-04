using Utils;
using System;
using Json2CSharp;
using UnityEngine;
using System.Collections.Generic;

public class ModulesManager : MonoBehaviour
{
    public AstroModule astro;

    public RainModule rain;

    public CloudsModule clouds;

    [Header("UI Modules"), Space] public InputModule input;

    public CityButtonsModule buttons;

    public ClockModule clock;

    private GameplayData gameplayData;

    private List<Api> api;

    private void Awake()
    {
        input.OnSet += (json, completed) => StartCoroutine(UNet.Fetch($"run.mocky.io/v3/{json}", (AppData data) =>
        {
            buttons.Populate(data.GetCitiesSortedByTime);

            api = data.api;
        }, completed));

        buttons.OnCityChange += city =>
        {
            DynamicWeatherController.Fetch(this, city, api, data =>
            {
                Player.IsDay(data.IsDay);

                Vehicle.SetActiveLights(data.IsDay);

                astro.Position = data.IsDay switch
                {
                    true => new Vector3(data.Location.Sun.Altitude, data.Location.Sun.Azimuth, data.Location.Sun.Distance),

                    null => new Vector3(data.Location.Sun.Altitude, data.Location.Sun.Azimuth, data.Location.Sun.Distance),

                    false => new Vector3(data.Location.Moon.Altitude, data.Location.Moon.Azimuth, data.Location.Moon.Distance)
                };

                astro.Shade = Tuple.Create(data.Location.WeatherDescription, data.Location.Time.Hour);

                clouds.Direction = data.Location.WindAngle;

                clouds.Speed = data.Location.WindSpeed;

                clouds.Density = data.Location.Clouds;

                rain.OneHour = data.Location.Rain;

                clouds.IsDay = data.IsDay;

                gameplayData = data;

#if UNITY_EDITOR
                UDebug.ClearConsole();
#endif
                Debug.Log(data);
            });

            clock.City = city;
        };
    }
}