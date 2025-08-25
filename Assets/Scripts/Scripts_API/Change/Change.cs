using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Networking;
using static change_imfomation;

public class Changes : MonoBehaviour
{
    public TMP_InputField name;
    public static int region;
    public static string regionName;
    public static string username;
    public GameRegister game;
    public GameObject notifications;
    public GameObject tbtk;
    public void bdd()
    {
        StartCoroutine(cgew());        
    }
    IEnumerator cgew()
    {
        region = GameRegion.selectedRegionId;
        regionName = GameRegion.regionName;
        tg.regionname = regionName;
        if (name.text == "")
        {
            notifications.GetComponentInChildren<TextMeshProUGUI>().text = "Nhập đầy đủ thông tin";
            yield break;
        }
        string Name = name.text;        
        string id = PlayerPrefs.GetString("UserId");
        chg ch = new chg(
            id,
            Name,
        region
            );        
        string body = JsonConvert.SerializeObject( ch );
        using (UnityWebRequest www = new UnityWebRequest("https://localhost:7038/api/APIGame/Change", "PUT"))
        {
            byte[] bodyraw = System.Text.Encoding.UTF8.GetBytes( body );
            www.uploadHandler = new UploadHandlerRaw( bodyraw );
            www.downloadHandler = new DownloadHandlerBuffer();
            www.SetRequestHeader( "Content-Type", "application/json" );
            yield return www.SendWebRequest();
            if (www.error != null)
            {
                Debug.Log("Response Code: " + www.responseCode);
                Debug.Log("Response Text: " + www.downloadHandler.text);  
            }
            else
            {                     
                PlayerPrefs.SetString("RegionName",regionName);
                tg.name = username;
                thongbao tb = JsonConvert.DeserializeObject<thongbao>(www.downloadHandler.text);
                notifications.GetComponentInChildren<TextMeshProUGUI>().text = tb.notification;                
                tbtk.GetComponentsInChildren<TextMeshProUGUI>()[1].text = "Name : " +Name;
                tbtk.GetComponentsInChildren<TextMeshProUGUI>()[2].text ="Region : "+ PlayerPrefs.GetString("RegionName");
            }
        }
    }
}
