using UnityEngine;
using System.Collections;
using System.Security.Cryptography;
using System.Text;

public class EncryptedPlayerPrefs
{

    public static string SetPrivatKey { set { privateKey = value; } }

    private static string privateKey = "";//8835408d0168446f8ba19605ebf468b3

    public static string[] keys;


    public static string Md5(string strToEncrypt)
    {
        UTF8Encoding ue = new UTF8Encoding();
        byte[] bytes = ue.GetBytes(strToEncrypt);

        MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
        byte[] hashBytes = md5.ComputeHash(bytes);

        string hashString = "";

        for (int i = 0; i < hashBytes.Length; i++)
        {
            hashString += System.Convert.ToString(hashBytes[i], 16).PadLeft(2, '0');
        }

        return hashString.PadLeft(32, '0');
    }

    public static void SaveEncryption(string key, string type, string value)
    {
        if (privateKey == string.Empty)
            Debug.Log("[SaveEncryption] privateKey is empty");

        int keyIndex = (int)Mathf.Floor(Random.value * keys.Length);
        string secretKey = keys[keyIndex];
        string check = Md5(type + "_" + privateKey + "_" + secretKey + "_" + value);
        PlayerPrefs.SetString(key + "_encryption_check", check);
        PlayerPrefs.SetInt(key + "_used_key", keyIndex);
    }

    public static bool CheckEncryption(string key, string type, string value)
    {
        if (privateKey == string.Empty)
            Debug.Log("[SaveEncryption] privateKey is empty");

        int keyIndex = PlayerPrefs.GetInt(key + "_used_key");
        string secretKey = keys[keyIndex];
        string check = Md5(type + "_" + privateKey + "_" + secretKey + "_" + value);
        if (!PlayerPrefs.HasKey(key + "_encryption_check")) return false;
        string storedCheck = PlayerPrefs.GetString(key + "_encryption_check");
        return storedCheck == check;
    }

    public static void SetInt(string key, int value)
    {
        PlayerPrefs.SetInt(key, value);
        SaveEncryption(key, "int", value.ToString());
    }

    public static void SetFloat(string key, float value)
    {
        PlayerPrefs.SetFloat(key, value);
        SaveEncryption(key, "float", Mathf.Floor(value * 1000).ToString());
    }

    public static void SetString(string key, string value)
    {
        PlayerPrefs.SetString(key, value);
        SaveEncryption(key, "string", value);
    }

    public static int GetInt(string key)
    {
        return GetInt(key, 0);
    }

    public static float GetFloat(string key)
    {
        return GetFloat(key, 0f);
    }

    public static string GetString(string key)
    {
        return GetString(key, "");
    }

    public static int GetInt(string key, int defaultValue)
    {
        int value = PlayerPrefs.GetInt(key);
        if (!CheckEncryption(key, "int", value.ToString())) return defaultValue;
        return value;
    }

    public static float GetFloat(string key, float defaultValue)
    {
        float value = PlayerPrefs.GetFloat(key);
        if (!CheckEncryption(key, "float", Mathf.Floor(value * 1000).ToString())) return defaultValue;
        return value;
    }

    public static float GetFloat(string key, float defaultValue, bool edited)
    {
        float value = PlayerPrefs.GetFloat(key);
        if (!CheckEncryption(key, "float", Mathf.Floor(value * 1000).ToString()))
        {
            edited = true;
            return defaultValue;
        }
        edited = false;
        return value;
    }

    public static string GetString(string key, string defaultValue)
    {
        string value = PlayerPrefs.GetString(key);
        if (!CheckEncryption(key, "string", value)) return defaultValue;
        return value;
    }

    public static bool HasKey(string key)
    {
        return PlayerPrefs.HasKey(key);
    }

    public static void DeleteKey(string key)
    {
        PlayerPrefs.DeleteKey(key);
        PlayerPrefs.DeleteKey(key + "_encryption_check");
        PlayerPrefs.DeleteKey(key + "_used_key");
    }

}