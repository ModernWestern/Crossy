using System;
using UnityEngine;
using Newtonsoft.Json;
using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;

namespace Utils
{
    public static class UBool
    {
        public static bool IsEven<T>(this T value) where T : struct, IComparable, IConvertible, IEquatable<T>
        {
            if (typeof(T) == typeof(decimal))
            {
#if UNITY_EDITOR
                Debug.LogError("Decimal type is not supported.");
#endif
                return false;
            }

            var intValue = Convert.ToInt32(value);

            return (intValue & 1) == 0;
        }
    }

    public static class UNet
    {
        private static void Response<T>(UnityWebRequest request, Action<T> response, Action<bool> success) where T : class
        {
            if (request.result is UnityWebRequest.Result.ConnectionError or UnityWebRequest.Result.ProtocolError)
            {
#if UNITY_EDITOR
                Debug.LogWarning(request.error);
#endif
                success?.Invoke(false);
            }
            else
            {
                if (typeof(T).IsAssignableFrom(typeof(string)))
                {
                    response?.Invoke(request.downloadHandler.text as T);

                    success?.Invoke(true);
                }
                else
                {
                    try
                    {
                        response?.Invoke(JsonConvert.DeserializeObject<T>(request.downloadHandler.text));

                        // response?.Invoke(JsonUtility.FromJson<T>(request.downloadHandler.text)); // Newtonsoft JsonProperty attribute do not work with JsonUtility.
                    }
                    catch (Exception ex)
                    {
#if UNITY_EDITOR
                        Debug.Log($"<color=red>An error occurred during deserialization of the object {typeof(T).Name}:</color> " + ex.Message);
#endif
                    }

                    success?.Invoke(true);
                }
            }
        }

        public static IEnumerator Fetch<T>(string url, Action<T> response, Action<bool> success = null) where T : class
        {
            var request = UnityWebRequest.Get(url);

            yield return request.SendWebRequest();

            Response(request, response, success);
        }

        public static IEnumerator Fetch<T>(string url, Action<T> response, params string[] headers) where T : class
        {
            var request = UnityWebRequest.Get(url);

            if (headers.Length.IsEven())
            {
                for (int i = 0, l = headers.Length; i < l; i += 2)
                {
                    var key = headers[i];

                    var value = headers[i + 1];

                    request.SetRequestHeader(key, value);
                }
            }

#if UNITY_EDITOR
            else
            {
                Debug.LogWarning("The header array must have an even number of elements. Some keys or values are missing.");
            }
#endif
            yield return request.SendWebRequest();

            Response(request, response, null);
        }

        public static IEnumerator Fetch<T>(string url, Action<T> response, Action<bool> success, params string[] headers) where T : class
        {
            var request = UnityWebRequest.Get(url);

            if (headers.Length.IsEven())
            {
                for (int i = 0, l = headers.Length; i < l; i += 2)
                {
                    var key = headers[i];

                    var value = headers[i + 1];

                    request.SetRequestHeader(key, value);
                }
            }

#if UNITY_EDITOR
            else
            {
                Debug.LogWarning("The header array must have an even number of elements. Some keys or values are missing.");
            }
#endif
            yield return request.SendWebRequest();

            Response(request, response, success);
        }
    }

    public static class UString
    {
        public static string ToFirstUpper(this string str)
        {
            return char.ToUpper(str[0]) + str[1..];
        }
    }

    public static class UEnum
    {
        public static string EnumToString<T>(this T enumValue) where T : Enum
        {
            return Enum.GetName(typeof(T), enumValue);
        }

        public static T ToEnum<T>(this string str) where T : Enum
        {
            return (T)Enum.Parse(typeof(T), str);
        }
    }

    public static class UMath
    {
        public static float Map(this float value, float inMin, float inMax, float outMin, float outMax)
        {
            var percentage = Mathf.InverseLerp(inMin, inMax, value);

            var mappedValue = Mathf.Lerp(outMin, outMax, percentage);

            return mappedValue;
        }

        public static float Map01(this float value, float outMin, float outMax)
        {
            var mappedValue = Mathf.Lerp(outMin, outMax, value);

            return mappedValue;
        }

        public static float LogarithmicMap(this float value, float inMin, float inMax, float outMin, float outMax, float exponent = 2)
        {
            var percentage = Mathf.InverseLerp(inMin, inMax, value);

            var mappedPercentage = Mathf.Pow(percentage, exponent);

            var mappedValue = Mathf.Lerp(outMin, outMax, mappedPercentage);

            return mappedValue;
        }

        public static float ExponentialMap(this float value, float inMin, float inMax, float outMin, float outMax, float exponent = 2)
        {
            var percentage = Mathf.InverseLerp(inMin, inMax, value);

            var mappedPercentage = Mathf.Pow(1f - percentage, exponent);

            var mappedValue = Mathf.Lerp(outMin, outMax, mappedPercentage);

            return mappedValue;
        }

        public static float CurveMap(this float value, float inMin, float inMax, float outMin, float outMax, AnimationCurve curve)
        {
            var percentage = Mathf.InverseLerp(inMin, inMax, value);

            var curveValue = curve.Evaluate(percentage);

            var mappedValue = Mathf.Lerp(outMin, outMax, curveValue);

            return mappedValue;
        }
    }

    public static class UDebug
    {
        public static void ClearConsole()
        {
            var logEntries = System.Type.GetType("UnityEditor.LogEntries,UnityEditor.dll");

            var clearMethod = logEntries.GetMethod("Clear", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public);

            clearMethod.Invoke(null, null);
        }
    }
    
    public static class ULeanTween
    {
#if LEANTWEEN
        public static void StoreUniqueId(this LTDescr descr, ref List<int> ids)
        {
            ids.Add(descr.uniqueId);
        }
#endif
    }
}