using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeSceneManager : MonoBehaviour
{
    public GameObject HomeScene;
    public GameObject Login;
    private void Start()
    {
        if (PlayerPrefs.GetInt("home", 0) == 1)
        {
            HomeScene.SetActive(true);
            Login.SetActive(false);
            PlayerPrefs.SetInt("home", 0);
        }
    }
}
