using Utils;
using TMPro;
using System;
using UnityEngine;
using Json2CSharp;
using System.Collections.Generic;

public class CityButtonsModule : MonoBehaviour
{
    public event Action<City> OnCityChange;

    [SerializeField] private Transform buttons;

    private GameObject buttonTemplate;

    private bool isPopulated;

    private void Awake()
    {
        buttonTemplate = buttons.GetChild(0).gameObject;

        buttonTemplate.SetActive(false);
    }

    public void Populate(List<City> cities)
    {
        if (isPopulated)
        {
            return;
        }

        for (int i = 0, l = cities.Count > 12 ? 12 : cities.Count; i < l; ++i)
        {
            var city = cities[i];

            var cityName = city.name.ToFirstUpper();

            var button = Instantiate(buttonTemplate, buttons).GetComponent<CityButton>();

            button.name = cityName;

            button.IsDay = city.IsDay;

            button.gameObject.SetActive(true);

            button.GetComponentInChildren<TMP_Text>().text = cityName;

            button.onClick.AddListener(() => OnCityChange?.Invoke(city));
        }

        isPopulated = true;
    }
}