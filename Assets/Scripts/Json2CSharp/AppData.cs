using System;
using System.Linq;
using System.Collections.Generic;

namespace Json2CSharp
{
    [Serializable]
    public class Api
    {
        public string name;
        public string url;
        public string key;
    }

    [Serializable]
    public class City
    {
        public string country;
        public string name;
        public float gmt;

        /// <summary>
        /// Internal use only.
        /// </summary>
        public bool? IsDay;
    }

    [Serializable]
    public class AppData
    {
        public List<Api> api;

        public List<City> cities;

        public List<City> GetCitiesSortedByTime => SortByTime(cities);

        private static List<City> SortByTime(IEnumerable<City> cities)
        {
            var times = cities.Select(city => new { City = city, Time = DateTime.UtcNow.AddHours(city.gmt).Hour });

            return times.OrderBy(c =>
            {
                c.City.IsDay = c.Time switch
                {
                    >= 5 and < 6 => true,
                    >= 6 and < 18 => null,
                    >= 18 and < 19 => true,
                    >= 0 and < 5 or >= 19 and < 24 => false,
                    _ => c.City.IsDay
                };

                return c.Time;
                
            }).Select(c => c.City).ToList();
        }
    }
}