using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audio : MonoBehaviour
{
    public AudioSource AudioSource;
    private Collider cl;    
    private void Start()
    {
        cl = GameObject.Find("Weapon").GetComponent<Collider>();
        cl.enabled = false;
    }    
    public void ado()
    {
        AudioSource.Play(); 
        cl.enabled = true;
        return;
    }
    public void DisableCollider()
    {
        cl.enabled = false;
    }
}
