using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Newtonsoft.Json;
using UnityEngine.Networking;
using System.Security.Principal;
using UnityEngine.Assertions.Must;

public class fg_ip_email : MonoBehaviour
{
    public TMP_InputField Inputemail;
    public GameObject notfications;
    public GameObject hotp;
    private string em;
    public void inputEmail()
    {
        StartCoroutine(email());
    }   
    IEnumerator email()
    {
        fg_email.ipem fg = new fg_email.ipem(eml());        
        if (fg == null)
        {
            Debug.Log("Nhập đủ thông tin");
            yield break;
        }
        string body = JsonConvert.SerializeObject(fg);
        Debug.Log("body :" + body);
        using (UnityWebRequest www = new UnityWebRequest("https://localhost:7038/api/APIGame/ForgotPassword", "POST"))
        {
            byte[] bodyraw = System.Text.Encoding.UTF8.GetBytes(body);
            www.uploadHandler = new UploadHandlerRaw(bodyraw);
            www.downloadHandler = new DownloadHandlerBuffer();            
            www.SetRequestHeader("Content-Type", "application/json");
            yield return www.SendWebRequest();
            if (www.error != null)
            {
                Debug.Log(www.error);                             
            }
            else
            {
                Debug.Log(www.downloadHandler.text);
                fg_email.kt nf = JsonConvert.DeserializeObject<fg_email.kt>(www.downloadHandler.text);
                notfications.GetComponentInChildren<TextMeshProUGUI>().text = nf.notification;
                hotp.SetActive(true);
                gameObject.SetActive(false);
            }
        }
    }
    public string eml()
    {
        string email = Inputemail.text;
        em = email;
        return em;
    }
}
