using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BGM : MonoBehaviour
{
    public AudioClip BackgroundMusic;
    public AudioSource audioSource;

    void Awake()
    {
            DontDestroyOnLoad(audioSource); //배경음악 계속 재생하게
    }

    public void StopBGM()
    {
        audioSource.Stop();
    }

    public void StartBGM()
    {
        audioSource.Play();
    }

}
