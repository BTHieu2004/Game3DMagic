using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Newtonsoft.Json;
using UnityEngine.Networking;
using Unity.VisualScripting;
using static weapondata;

public class DeletedWeapons : MonoBehaviour
{
    public TMP_InputField weapon;
    public GameObject notifications;
    public GameObject Weapons;    
    public Game_Weapons Game_Weapons;
    public GameObject weapons;
    public void deleted()
    {        
        StartCoroutine(dl());        
    }
    public void h_weapons()
    {                
        gameObject.SetActive(false);
        Weapons.SetActive(true);
    }
    IEnumerator dl()
    {
        string wap = weapon.text;
        if (weapon.text == "")
        {
            notifications.GetComponent<TextMeshProUGUI>().text = "Vui lòng nhập thông tin";
            yield break;
        }
        weapondata.Weapondatas weapo = new weapondata.Weapondatas(wap);
        string body = JsonConvert.SerializeObject(weapo);
        Debug.Log(body);
        using (UnityWebRequest www = new UnityWebRequest("https://localhost:7038/api/APIGame/DeletedVk","PUT"))
        {
            byte[] bodyraw = System.Text.Encoding.UTF8.GetBytes(body);
            www.uploadHandler = new UploadHandlerRaw(bodyraw);
            www.downloadHandler = new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");
            yield return www.SendWebRequest();
            if (www.error != null)
            {                
                Debug.Log(www.error);
                Debug.Log("Response Code: " + www.responseCode);                
            }
            else
            {
                Debug.Log(www.downloadHandler.text);
                weapondata.tbx tba= JsonConvert.DeserializeObject<weapondata.tbx>(www.downloadHandler.text);
                notifications.GetComponent<TextMeshProUGUI>().text = tba.notification;
                Game_Weapons.RemoveWeaponById(wap,tba.data.id);
            }
        }
    }
}
