using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoMain : MonoBehaviour
{
    public void GoMainBtn()
    {
        SceneManager.LoadScene("MainScene");
        Debug.Log("메인으로");
    }
}
