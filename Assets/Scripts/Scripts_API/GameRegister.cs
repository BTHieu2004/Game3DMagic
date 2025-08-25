using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using TMPro;
using UnityEngine.Networking;
using System.Linq;
using UnityEngine.UI;
public class GameRegister : MonoBehaviour
{
    public TMP_InputField emailInput;
    public TMP_InputField passwordInput;
    public TMP_InputField nameInput;
    public GameObject notfication;
    public static int selectedRegionId;
    public static string username;
    public GameObject Login;
    public GameObject thongtintk;
    private string tk;
    private string tk2;
    public static string regionName;    
    public void hthongtintk()
    {
        thongtintk.SetActive(true);
        gameObject.SetActive(false);        
    }
    public void hlg()
    {        
        Login.SetActive(true);
        gameObject.SetActive(false);
    }
    public void register()
    {        
        thongtintk.SetActive(false);
    }
    public void OnButtonClickRegister()
    {
        StartCoroutine(Registers());
    }
    public void anpn()
    {
        notfication.SetActive(false);
    }
    IEnumerator Registers()
    {
        regionName = GameRegion.regionName;
        tg.regionname = regionName;
        selectedRegionId = GameRegion.selectedRegionId;
        Register.RegisterRequestData requestData = new Register.RegisterRequestData(
            emailInput.text,
            passwordInput.text, 
            nameInput.text,"",
            selectedRegionId
            );
        //string body = JsonUtility.ToJson(requestData);
        string body = JsonConvert.SerializeObject(requestData); 
        Debug.Log("Request Body: " + body);  // Kiểm tra JSON

        if (emailInput.text == "" || passwordInput.text == "" || nameInput.text == "")
        {
            notfication.SetActive(true);
            notfication.GetComponentsInChildren<TMP_Text>()[1].text = "Vui lòng nhập đầy đủ thông tin!";
            yield break;
        }
        using (UnityWebRequest www = new UnityWebRequest("https://localhost:7038/api/APIGame/Register", "POST"))
        {            
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(body);
            www.uploadHandler = new UploadHandlerRaw(bodyRaw);
            www.downloadHandler = new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");            
            yield return www.SendWebRequest();
            if (www.error!=null)
            {                
                Debug.Log(www.error);
                string responseJson = www.downloadHandler.text;
                Debug.Log(responseJson);
                Register.ResponseUserError responseErr = JsonConvert.DeserializeObject<Register.ResponseUserError>(responseJson);                
                if (responseErr != null && responseErr.data!=null&&responseErr.data.Count>0)
                {                    
                    notfication.SetActive(true);
                    var textComponents = notfication.GetComponentsInChildren<TMP_Text>();
                    string error = string.Join(", ",responseErr.data.Select(e=>e.description));
                    if (textComponents.Length > 1)
                    {
                        textComponents[1].text = error;
                    }
                }               
            }
            else
            {          
                PlayerPrefs.SetString("RegionName",regionName);
                Debug.Log("Response JSON: " + www.downloadHandler.text);  
                string json = www.downloadHandler.text;     
                taikhoan.tk td = JsonConvert.DeserializeObject<taikhoan.tk>(json);
                tk = td.data.id;
                Register.ResponseUserSuccess response = JsonConvert.DeserializeObject<Register.ResponseUserSuccess>(json);
                var data = response.data;
                Debug.Log(data);
                if (response.isSuccess)
                {                                                      
                    notfication.SetActive(true);
                    notfication.GetComponentsInChildren<TMP_Text>()[1].text =
                        "Đăng ký thành công, vui lòng quay lại trang đăng nhập!" + data.name;
                    thongtintk.GetComponentsInChildren<TMP_Text>()[0].text = "ID : " +td.data.id;
                    thongtintk.GetComponentsInChildren<TMP_Text>()[1].text = "Name : " + td.data.name;
                    //thongtintk.GetComponentsInChildren<TMP_Text>()[2].text = "Region :" + td.data.regionId.ToString();
                    thongtintk.GetComponentsInChildren<TMP_Text>()[2].text = "Region :" + regionName;
                }
            }
        }        
    }    
    public string idtk()
    {
        tk2 = tk;
        return tk2;
    }
}
