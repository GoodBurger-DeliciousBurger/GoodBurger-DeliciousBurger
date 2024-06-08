using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BGM : MonoBehaviour
{
    public AudioSource audioSource;

    void Awake()
    {
        if (audioSource != null) DontDestroyOnLoad(audioSource); //배경음악 계속 재생하게
    }

    public void StopBGM()
    {
        audioSource.Pause();
    }

    public void StartBGM()
    {
        audioSource.UnPause();
    }

}
