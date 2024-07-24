using UnityEngine;
using UnityEngine.AddressableAssets;
 
public class SingletonScriptable<T> : ScriptableObject where T : SingletonScriptable<T>
{
    private static T instance;
    public static T i
    {
        get
        {
            if (instance == null)
            {
                var op = Addressables.LoadAssetAsync<T>(typeof(T).Name);
 
                instance = op.WaitForCompletion();
            }
            return instance;
        }
    }
}