using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using static input_OTP;
using Newtonsoft.Json;
using UnityEngine.Networking;
using UnityEngine.EventSystems;
public class nhapOTP : MonoBehaviour
{
    public TMP_InputField inputOtp;
    public TMP_InputField inputPassword;
    public GameObject notification;
    public fg_ip_email fg_Ip_Email;    
    public void eset()
    {
        StartCoroutine(rs());
    }  
    IEnumerator rs()
    {
        int otp = int.Parse(inputOtp.text);
        string password = inputPassword.text;
        string email = fg_Ip_Email.eml();        
        ipOTP ipotp = new ipOTP(
            otp ,
            password,
        email
            );
        if (password == "" || email=="")
        {            
            notification.GetComponentsInChildren<TMP_Text>()[1].text = "Vui lòng nhập đầy đủ thông tin!";
            yield break;
        }
        string body = JsonConvert.SerializeObject(ipotp);
        using (UnityWebRequest www = new UnityWebRequest("https://localhost:7038/api/APIGame/ResetPassword", "POST"))
        {
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(body);
            www.uploadHandler = new UploadHandlerRaw(bodyRaw);
            www.downloadHandler = new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");
            yield return www.SendWebRequest();
            if (www.error!=null)
            {
                Debug.Log(www.error);
                htt ntf = JsonConvert.DeserializeObject<htt>(www.downloadHandler.text);
                notification.GetComponentInChildren<TextMeshProUGUI>().text = ntf.notification;
            }
            else
            {
                htt ntf = JsonConvert.DeserializeObject<htt>(www.downloadHandler.text);
                notification.GetComponentInChildren<TextMeshProUGUI>().text = ntf.notification;
                Debug.Log(email);
            }
        }
    }
}
