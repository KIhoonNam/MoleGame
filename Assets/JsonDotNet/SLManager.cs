using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;



public class SLManager : MonoBehaviour
{


    public static void Save()
    {
        try
        {
            string jdata = JsonConvert.SerializeObject(GameManger.instance.GetPlayerStat());
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(jdata);
            string format = System.Convert.ToBase64String(bytes);
            File.WriteAllText(Application.persistentDataPath + "/TikTokTok.json", format);
            Debug.Log(Application.persistentDataPath);
        }
        catch(System.Exception e)
        {
            Debug.Log(e);
        }
    }

    public static void Load()
    {
        try
        {
            string jdata = File.ReadAllText(Application.persistentDataPath + "/TikTokTok.json");
            byte[] bytes = System.Convert.FromBase64String(jdata);
            string format = System.Text.Encoding.UTF8.GetString(bytes);

            GameManger.instance.SetPlayerStat(JsonConvert.DeserializeObject<PlayerState>(format));
        }
        catch(System.Exception e)
        {
            GameManger.instance.SetPlayerStat(new PlayerState("Nam","ska960812@gmail.com",1, 0, 0, 0,1));
        }
    }
}
