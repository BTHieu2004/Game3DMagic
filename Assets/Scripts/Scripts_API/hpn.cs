using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class hpn : MonoBehaviour
{
    public GameObject pn;
    public GameObject login;
    public GameObject weap;
    public GameObject logout;
    public GameObject fg;
    public GameObject otp;
    public void anpn()
    {
        pn.SetActive(true);
        gameObject.SetActive(false);
    }
    public void Vescene()
    {
        SceneManager.LoadScene(1);        
    }
    public void lg()
    {        
        weap.SetActive(false);
    }
    public void Logout()
    {
        logout.SetActive(false);
        gameObject.SetActive(true );
    }
    public void fg_lg()
    {
        pn.SetActive(false);
        login.SetActive(true);
    }
    public void otp_lg() {
        otp.SetActive(false);
        pn.SetActive(false);
        fg.SetActive(true);
        login.SetActive(true);
    }
}
