using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameRules : MonoBehaviour
{
    public void GameRulesBtn()
    {
        SceneManager.LoadScene("GameRulesScene");
        Debug.Log("게임방법");
    }
}
