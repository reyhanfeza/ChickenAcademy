using UnityEngine;

namespace Base.Runtime.Services.Data
{
    public static class DataService
    {
        public static T LoadObjectWithKey<T>(string key) where T : new()
        {
            T cachedClass = JsonUtility.FromJson<T>(PlayerPrefs.GetString(key));

            if (cachedClass == null)
            {
                cachedClass = new T();
                Debug.Log($"Data Not Found. Type : {typeof(T)}");
            }
            else
            {
                Debug.Log($"Data Found. Type : {typeof(T)} JSON Data : {PlayerPrefs.GetString(key)}");
            }

            return cachedClass;
        }

        public static void SaveDataAsJson(string key, object data)
        {
            PlayerPrefs.SetString(key, ObjectToJson(data));
        }

        public static string ObjectToJson(object targetObject)
        {
            return JsonUtility.ToJson(targetObject);
        }
    }  
}