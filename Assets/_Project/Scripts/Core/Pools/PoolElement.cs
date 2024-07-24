using System;
using UnityEngine;
using System.Collections;

namespace FunnyBlox.Utils
{
    public class PoolElement
    {
        public bool IsBusy { get; set; }

        public Transform Transform { get; set; }

        public Component Component { get; set; }

        /*public T Script<T>() where T : class
        {
           
            return mScript;
        }
        
        
        public static T Load<T>(string key)
        {
            T data = default;
    
            if (PlayerPrefs.HasKey(key))
            {
                string loadedJson = PlayerPrefs.GetString(key);
                data = JsonConvert.DeserializeObject<T>(loadedJson);
            }
            else
            {
                Save(key, data);
                data = Load<T>(key);
            }
            
            return data;
        }
        */
    }
}