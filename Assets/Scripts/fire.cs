using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Fire : MonoBehaviour
{    
    [SerializeField] private ParticleSystem ptc;
    [SerializeField] private AudioSource AudioSource;
    public static event Action activateAllfire;
    public static event Action deactivateAllfire;
    private void OnEnable()
    {
        activateAllfire += OnEvent;
        deactivateAllfire += rs;
    }
    private void OnDisable()
    {
        activateAllfire -= OnEvent;
        deactivateAllfire -= rs;
    }
    void Start()
    {
        ptc.Stop(); 
        AudioSource.Stop();
    }
    public void rs()
    {
        ptc.Stop();
        AudioSource.Stop();
    }
    private void OnEvent()
    {
        ptc.Play();
        AudioSource.Play();
    }
    public static void Action()
    {
        activateAllfire?.Invoke();
    }
    public static void reset()
    {
        deactivateAllfire?.Invoke();
    }


}
