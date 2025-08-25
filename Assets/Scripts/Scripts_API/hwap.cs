using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hwap : MonoBehaviour
{
    public GameObject wap;
    public GameObject deletedwp;
    public GameObject notifi;
    public void ap()
    {
        wap.SetActive(true);
        gameObject.SetActive(false);
    }
    public void dl()
    {        
        deletedwp.SetActive(true);
    }
    public void disnotifi()
    {
        notifi.SetActive(false);
        gameObject.SetActive(true );
    }
}
