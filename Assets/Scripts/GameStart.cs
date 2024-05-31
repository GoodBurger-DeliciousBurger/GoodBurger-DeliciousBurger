using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour
{
    public void GameStartBtn()
    {
        SceneManager.LoadScene("MainGameScene");
        Debug.Log("게임시작");
    }
}
