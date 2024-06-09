using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoRecipeScene : MonoBehaviour
{
    private static string previousScene;
    
    //현재 씬 이름 저장 후 다음 씬 전환
    public void GoRecipeBtn()
    {
        previousScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene("RecipeScene");
    }

    // 저장된 이전 씬으로 돌아가기
    public void GoBackPreviousScene()
    {
        if(!string.IsNullOrEmpty(previousScene))
        {
            SceneManager.LoadScene(previousScene);
        }
        else
        {
            Debug.Log("이전 씬 이름 저장 안됨");
        }
    }

}
