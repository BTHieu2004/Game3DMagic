using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using UnityEngine.Networking;
using System;
using UnityEngine.UI;
using TMPro;
public class GameLevel : MonoBehaviour
{
    public GameObject PrefabGameLevel;
    public RectTransform ParentGameLevel;
    private void Start()
    {
        StartCoroutine(GetRequestAPIGameLevel());
    }    
    private IEnumerator GetRequestAPIGameLevel()
    {
        using (UnityWebRequest www = UnityWebRequest.Get("https://localhost:7038/api/APIGame/GetAllGameLevel"))
        {            
            yield return www.SendWebRequest();
            if (www.result == UnityWebRequest.Result.ConnectionError||
                www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("API call failed: " + www.error);
            }
            else
            {
                Debug.Log("JSON Response: " + www.downloadHandler.text);
                try
                {
                    Scripts.ResponseAPI response = JsonConvert.DeserializeObject<Scripts.ResponseAPI>(www.downloadHandler.text);
                    HandleGetResponseLevel(response);

                }
                catch (Exception ex)
                {
                    Debug.LogError("Exception during Json parsing" + ex.Message);
                }
            }
        }        
    }
    public void HandleGetResponseLevel(Scripts.ResponseAPI response)
    {
        if (response != null && response.isSuccess)
        {

            Debug.Log(response);
            if (response.data != null)
            {
                Debug.Log(response.data);
                foreach (var level in response.data)
                {
                    GameObject game = Instantiate(PrefabGameLevel, ParentGameLevel);
                    game.GetComponent<LevelGameData>().LevelId = level.levelId;
                    game.GetComponent<LevelGameData>().title = level.title;
                    game.GetComponent<LevelGameData>().description = level.description;
                    game.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = level.title;
                }
            }
        }
    }
}
