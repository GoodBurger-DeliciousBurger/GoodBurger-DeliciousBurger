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
        
            DontDestroyOnLoad(audioSource); //배경음악 계속 재생하게(이후 버튼매니저에서 조작)
        
    }

}
