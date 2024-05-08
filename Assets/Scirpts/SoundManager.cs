using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private AudioSource audioSource;

    public AudioSource BGM;
    public AudioSource time_Audiosource;
    public AudioClip button_Click_SFX;
    public AudioClip jump_SFX;
    public AudioClip time_SFX;
    public AudioClip crash_SFX;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void Button_Click()
    {
        audioSource.clip = button_Click_SFX;
        audioSource.Play();
    }

    public void Jump()
    {
        audioSource.clip = jump_SFX;
        audioSource.Play();
    }
    
    public void time_Flow()
    {
        time_Audiosource.clip = time_SFX;
        time_Audiosource.Play();
    }

    public void Crash()
    {
        audioSource.clip = crash_SFX;
        audioSource.Play();
    }


    public void BGM_Stop()
    {
        BGM.Stop();
    }
}
