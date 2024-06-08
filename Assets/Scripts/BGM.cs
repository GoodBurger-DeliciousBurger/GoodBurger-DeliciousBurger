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
            DontDestroyOnLoad(audioSource); //������� ��� ����ϰ�
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
