using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] private GameObject _panel;
    [SerializeField] private GameObject amthanh;
    //[SerializeField] private GameObject die;
    [SerializeField] private AudioMixer My_AudioMixer;
    [SerializeField] private Slider music_slider;
    [SerializeField] private Slider SFX_slider;    
    private void Start()
    {
        if (PlayerPrefs.HasKey("musicVolume")|| PlayerPrefs.HasKey("musicVolume"))
        {
            LoadVolum();
        }
        else
        {
            SetMusicVolume();
            SetSFXVolume();
        }        
    }
    public void SetMusicVolume()
    {
        float volume = music_slider.value;
        My_AudioMixer.SetFloat("Music",Mathf.Log10(volume)*20);
        PlayerPrefs.SetFloat("musicVolume", volume);
    }
    public void SetSFXVolume()
    {
        float volume = SFX_slider.value;
        My_AudioMixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("SFXVolume", volume);
    }
    private void LoadVolum()
    {
        music_slider.value = PlayerPrefs.GetFloat("musicVolume");
        music_slider.value = PlayerPrefs.GetFloat("SFXVolume");
        SetMusicVolume();
        SetSFXVolume();
    }
    public void hien_pn()
    {
        _panel.SetActive(true);
        Time.timeScale = 0f;

    }
    public void tiep_tuc()
    {
        _panel.SetActive(false);
        Time.timeScale = 1.0f;
    }
    public void _home()
    {
        Time.timeScale = 1f;
        PlayerPrefs.SetInt("home", 1);
        SceneManager.LoadScene(0);
    }
    public void Reset()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(1);
    }
    public void setting_sound()
    {
        amthanh.SetActive(true);
    }
    public void an_setting_sound()
    {
        amthanh.SetActive(false );
    }
}
