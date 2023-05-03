using System;
using UnityEngine;
using Newtonsoft.Json;
using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;

namespace Utils
{
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

    public static class UDebug
    {
        public static void ClearConsole()
        {
            var logEntries = System.Type.GetType("UnityEditor.LogEntries,UnityEditor.dll");

            var clearMethod = logEntries.GetMethod("Clear", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public);

            clearMethod.Invoke(null, null);
        }
    }
}