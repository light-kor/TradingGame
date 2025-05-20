using Unity.Plastic.Newtonsoft.Json;
using UnityEngine;

namespace Common
{
    public class PlayerPrefsUtils
    {
        public static bool HasKey(string key)
        {
            return PlayerPrefs.HasKey(key);
        }
        
        public static void SaveInt(string key, int value)
        {
            PlayerPrefs.SetInt(key, value);
            PlayerPrefs.Save();
        }
        
        public static int LoadInt(string key)
        {
            return PlayerPrefs.GetInt(key);
        }
        
        public static void SaveFloat(string key, int value)
        {
            PlayerPrefs.SetFloat(key, value);
            PlayerPrefs.Save();
        }
        
        public static float LoadFloat(string key)
        {
            return PlayerPrefs.GetFloat(key);
        }
        
        public static bool TryLoadObject<T>(string key, out T value)
        {
            value = default;
            string json = PlayerPrefs.GetString(key);

            if (string.IsNullOrEmpty(json))
                return false;

            if (json == "{}")
            {
                Debug.LogError("Empty json deserialized");
                return false;
            }

            try
            {
                value = JsonConvert.DeserializeObject<T>(json);
                return value != null;
            }
            catch (JsonException ex)
            {
                Debug.LogError($"Error deserializing json: {ex.Message}");
                return false;
            }
        }

        public static void SaveObject<T>(string key, T value)
        {
            string json = JsonConvert.SerializeObject(value);
            PlayerPrefs.SetString(key, json);
            PlayerPrefs.Save();
        }
    }
}