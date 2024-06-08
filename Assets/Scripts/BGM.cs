using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BGM : MonoBehaviour
{
    public AudioSource audioSource;

    void Awake()
    {
        if (audioSource != null) DontDestroyOnLoad(audioSource); //������� ��� ����ϰ�
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
