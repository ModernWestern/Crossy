using Utils;
using Json2CSharp;
using UnityEngine;
using System.Collections.Generic;

public class ModulesManager : MonoBehaviour
{
    public PlayerEvents events;

    [Header("Modules"), Space] public AstroModule astro;

    public RainModule rain;

    public CloudsModule clouds;

    [Header("UI Modules"), Space] public InputModule input;

    public CityButtonsModule buttons;

    public ClockModule clock;

    private List<Api> api;

    private void Awake()
    {
        input.OnSet += (json, completed) => StartCoroutine(UNet.Fetch(json.Contains("mocky") ? json : $"run.mocky.io/v3/{json}", (AppData data) =>
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

                Trunk.SetRainAmount(data.Location.Rain);

                astro.Position = data.IsDay switch
                {
                    true => new Vector3(data.Location.Sun.Altitude, data.Location.Sun.Azimuth, data.Location.Sun.Distance),

                    null => new Vector3(data.Location.Sun.Altitude, data.Location.Sun.Azimuth, data.Location.Sun.Distance),

                    false => new Vector3(data.Location.Moon.Altitude, data.Location.Moon.Azimuth, data.Location.Moon.Distance)
                };

                astro.Shade = (data.Location.Time.Hour, data.Location.Rain);

                clouds.Direction = data.Location.WindAngle;

                clouds.Speed = data.Location.WindSpeed;

                clouds.Density = data.Location.Clouds;

                rain.OneHour = data.Location.Rain;

                clouds.IsDay = data.IsDay;

                events.CityChange(data);

                events.Start();

#if UNITY_EDITOR
                UDebug.ClearConsole();

                Debug.Log(data);
#endif
            });

            clock.City = city;
        };

        input.OnSkip += () => events.Start();
    }
}