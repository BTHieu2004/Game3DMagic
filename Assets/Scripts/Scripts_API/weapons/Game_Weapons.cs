using Newtonsoft.Json;
using Newtonsoft.Json.Bson;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using static ds;

public class Game_Weapons : MonoBehaviour
{
    public GameObject weapons;
    public RectTransform RectTransform;
    public GameObject deletedweapons;

    private List<GameObject> weaponList = new List<GameObject>();
    public void deleted()
    {
        gameObject.SetActive(false);
        deletedweapons.SetActive(true);        
    }
    public void Start()
    {
        StartCoroutine(wap());
    }
    public void bd()
    {
        StartCoroutine(wap());
    }
    IEnumerator wap()
    {
        using (UnityWebRequest www = UnityWebRequest.Get("https://localhost:7038/api/APIGame/GetAllVukhi"))
        {            
            yield return www.SendWebRequest();
            if (www.error != null)
            {
                Debug.LogError(www.error);
            }
            else
            {                
                inputweapons tp = JsonConvert.DeserializeObject<inputweapons>(www.downloadHandler.text);                
                handel(tp);
            }
        }
    }
    public void handel(inputweapons ip)
    {
        if(ip!=null && ip.isSuccess)
        {
            if (ip.data != null)
            {
                foreach (var weapon in ip.data)
                {
                    GameObject weapo = Instantiate(weapons,RectTransform);                                             
                    weapo.GetComponentsInChildren<TextMeshProUGUI>()[0].text = weapon.name;
                    weapo.GetComponentsInChildren<TextMeshProUGUI>()[1].text = weapon.damge.ToString();
                    weaponList.Add(weapo);
                }
            }
        }
    }
    public void RemoveWeaponById(string weaponId,int id)
    {
               
        GameObject weaponToRemove = weaponList.Find(w => w.GetComponentsInChildren<TextMeshProUGUI>()[0].text.Contains(weaponId));
        if (weaponToRemove != null)
        {
            weaponList.Remove(weaponToRemove); // Xóa khỏi danh sách
            Destroy(weaponToRemove); // Xóa khỏi scene
            Debug.Log("Weapon removed: " + weaponId);
        }
        else
        {
            Debug.Log("Weapon with ID " + weaponId + " not found.");
        }
    }
}
