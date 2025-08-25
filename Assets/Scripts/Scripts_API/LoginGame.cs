using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System;
using UnityEngine.Networking;
using static Login;
using UnityEngine.SceneManagement;
using System.Security.Cryptography;
using UnityEngine.Timeline;
using TMPro;

public class LoginGame : MonoBehaviour
{
    public TMPro.TMP_InputField emailInput;
    public TMPro.TMP_InputField passwordInput;
    public GameObject notfication;
    public GameObject pn;
    public GameObject register;
    private string baseUrl = "https://localhost:7038";
    public GameObject tttk;
    public static string regionName;
    private void Awake()
    {
        /*  if (PlayerPrefs.HasKey("token"))
        {
            SceneManager.LoadScene(1);
        }*/
        baseUrl = "https://localhost:7038";
    }
    IEnumerator login()
    {
        regionName = GameRegion.regionName;
        string email = emailInput.text;
        string password = passwordInput.text;
        RequestLoginData loginData = new RequestLoginData(email, password);
        string body = JsonConvert.SerializeObject(loginData);
        using (UnityWebRequest www = new UnityWebRequest(baseUrl + "/api/APIGame/Login", "POST"))
        {            
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(body);  
            www.uploadHandler = new UploadHandlerRaw(bodyRaw);
            www.downloadHandler = new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");
            yield return www.SendWebRequest();
            if (www.error != null) 
            {
                Debug.Log(www.downloadHandler.text);
                ResponseLogin response = JsonConvert.DeserializeObject<ResponseLogin>(www.downloadHandler.text);
                notfication.SetActive(true);
                notfication.GetComponentsInChildren<TextMeshProUGUI>()[1].text = response.notification;
            }
            else
            {
                ResponseLogin response = JsonConvert.DeserializeObject<ResponseLogin>(www.downloadHandler.text);
                if (response.isSuccess)
                {
                    PlayerPrefs.SetString("token", response.data.token);
                    PlayerPrefs.SetString("UserId", response.data.user.id);
                    PlayerPrefs.SetString("email", response.data.user.email);
                    PlayerPrefs.SetString("name", response.data.user.name);                    
                    PlayerPrefs.SetString("LinkAvatar", baseUrl+"/uploads/avatars/"+response.data.user.avatar);
                    PlayerPrefs.SetInt("regionId", response.data.user.regionId);
                    //notfication.SetActive(true);
                    tttk.GetComponentsInChildren<TextMeshProUGUI>()[0].text = PlayerPrefs.GetString("UserId");
                    tttk.GetComponentsInChildren<TextMeshProUGUI>()[1].text = PlayerPrefs.GetString("name");
                    tttk.GetComponentsInChildren<TextMeshProUGUI>()[2].text = PlayerPrefs.GetString("RegionName");
                    notfication.GetComponentsInChildren<TextMeshProUGUI>()[1].text = "Login success";
                    pn.SetActive(true);    
                    gameObject.SetActive(false);

                }
                else
                {
                    notfication.SetActive(true);
                    gameObject.SetActive (false);
                    notfication.GetComponentsInChildren<TextMeshProUGUI>()[1].text = response.notification;                    
                }
            }
        }
    }
    public void OnLoginButtonClicked()
    {
        StartCoroutine(login());
    }
    public void anotfication()
    {
        notfication.SetActive(false);
    } 
    public void Register()
    {
        register.SetActive(true);
        gameObject.SetActive(false);
    }
}
