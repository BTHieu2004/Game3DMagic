using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("-----Audio Souce-----")]
    [SerializeField] private AudioSource music_Source;
    [SerializeField] private AudioSource SFX_Source;
    [Header("-----Audio clip-----")]
    public AudioClip BackGround;
    public AudioClip die;
    public AudioClip potal;
    public AudioClip Electric;
    public AudioClip zzzzz;
    public void Start()
    {
        music_Source.clip = BackGround;
        music_Source.Play();
    }
    public void PlaySFX(AudioClip clip)
    {
        SFX_Source.PlayOneShot(clip);
    }
}
