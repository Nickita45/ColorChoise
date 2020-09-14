using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Security.Cryptography;

public class PlayerPrefsSafe : MonoBehaviour
{
     private const int salt = 54534645;

    public static void SetInt(string key, int value)
    {
        int salted = value ^ salt;
        PlayerPrefs.SetInt(StringHash(key), salted);
        PlayerPrefs.SetInt(StringHash("_" + key), IntHash(value));
    }

    public static int GetInt(string key)
    {
        return GetInt(key, 0);
    }

    public static int GetInt(string key, int defaultValue)
    {
        string hashedKey = StringHash(key);
        if (!PlayerPrefs.HasKey(hashedKey)) return defaultValue;

        int salted = PlayerPrefs.GetInt(hashedKey);
        int value = salted ^ salt;

        int loadedHash = PlayerPrefs.GetInt(StringHash("_" + key));
        if (loadedHash != IntHash(value)) return defaultValue;

        return value;
    }
    public static void SetString(string key, string value)
    {
        System.Text.Encoding enc = System.Text.Encoding.GetEncoding(28591);
        //byte[] bts = enc.GetBytes(value);
        //print(enc.GetString(bts));

        //int intValue = System.BitConverter.ToInt32(enc.GetBytes(value), 0);
        string strValue = EncryptDecrypt(value,salt);
        //print(intValue);
        //int salted = intValue ^ salt;
        PlayerPrefs.SetString(StringHash(key), strValue);
        PlayerPrefs.SetString(StringHash("_" + key), StringHash(strValue));
        
        //int value1 = salted ^ salt;
        //print(enc.GetString(System.BitConverter.GetBytes(value1)));
    }
    public static string GetString(string key)
    {
        return GetString(key, "None");
    }
    public static string GetString(string key, string defaultValue)
    {
        string hashedKey = StringHash(key);
        if (!PlayerPrefs.HasKey(hashedKey)) return defaultValue;

        string salted = PlayerPrefs.GetString(hashedKey);
        string resultValue =  EncryptDecrypt(salted,salt);
        
        string loadedHash = PlayerPrefs.GetString(StringHash("_" + key));
        if (loadedHash != StringHash(salted)) return defaultValue;

        return resultValue;
    }
    public static void SetFloat(string key, float value)
    {
        int intValue = System.BitConverter.ToInt32(System.BitConverter.GetBytes(value), 0);

        int salted = intValue ^ salt;
        PlayerPrefs.SetInt(StringHash(key), salted);
        PlayerPrefs.SetInt(StringHash("_" + key), IntHash(intValue));
    }

    public static float GetFloat(string key)
    {
        return GetFloat(key, 0);
    }

    public static float GetFloat(string key, float defaultValue)
    {
        string hashedKey = StringHash(key);
        if (!PlayerPrefs.HasKey(hashedKey)) return defaultValue;

        int salted = PlayerPrefs.GetInt(hashedKey);
        int value = salted ^ salt;

        int loadedHash = PlayerPrefs.GetInt(StringHash("_" + key));
        if (loadedHash != IntHash(value)) return defaultValue;

        return System.BitConverter.ToSingle(System.BitConverter.GetBytes(value), 0);
    }

    private static int IntHash(int x)
    {
        x = ((x >> 16) ^ x) * 0x45d9f3b;
        x = ((x >> 16) ^ x) * 0x45d9f3b;
        x = (x >> 16) ^ x;
        return x;
    }

    public static string StringHash(string x)
    {
        HashAlgorithm algorithm = SHA256.Create();
        System.Text.StringBuilder sb = new System.Text.StringBuilder();

        var bytes = algorithm.ComputeHash(System.Text.Encoding.UTF8.GetBytes(x));
        foreach (byte b in bytes) sb.Append(b.ToString("X2"));

        return sb.ToString();
    }

    public static void DeleteKey(string key)
    {
        PlayerPrefs.DeleteKey(StringHash(key));
        PlayerPrefs.DeleteKey(StringHash("_" + key));
    }

    public static bool HasKey(string key)
    {
        string hashedKey = StringHash(key);
        if (!PlayerPrefs.HasKey(hashedKey)) return false;

        int salted = PlayerPrefs.GetInt(hashedKey);
        int value = salted ^ salt;

        int loadedHash = PlayerPrefs.GetInt(StringHash("_" + key));

        return loadedHash == IntHash(value);
    }
    public static string EncryptDecrypt(string szPlainText, int szEncryptionKey)  
     {  
       System.Text.StringBuilder szInputStringBuild = new System.Text.StringBuilder(szPlainText);  
       System.Text.StringBuilder szOutStringBuild = new System.Text.StringBuilder(szPlainText.Length);  
       char Textch;  
       for (int iCount = 0; iCount < szPlainText.Length; iCount++)  
       {  
         Textch = szInputStringBuild[iCount];  
         Textch = (char)(Textch ^ szEncryptionKey);  
         szOutStringBuild.Append(Textch);  
       }  
       return szOutStringBuild.ToString();  
     } 
}
