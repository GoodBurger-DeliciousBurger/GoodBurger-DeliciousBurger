using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoRecipeScene : MonoBehaviour
{
    private static string previousScene;
    private const string SavedTimeKey = "SavedTime";
    private const string PreviousSceneKey = "PreviousScene";

    public void GoRecipeBtn()
    {
        // 현재 씬 이름 저장
        previousScene = SceneManager.GetActiveScene().name;
        PlayerPrefs.SetString(PreviousSceneKey, previousScene);

        // 현재 시간 저장
        if (previousScene == "MainGameScene")
        {
            PlayerPrefs.SetInt(SavedTimeKey, FindObjectOfType<Test>().GetSavedTime());
        }

        // 레시피 씬 로드
        SceneManager.LoadScene("RecipeScene");
    }

    public void GoBackPreviousScene()
    {
        previousScene = PlayerPrefs.GetString(PreviousSceneKey);

        if (!string.IsNullOrEmpty(previousScene))
        {
            SceneManager.LoadScene(previousScene);
        }
        else
        {
            Debug.Log("이전 씬 이름 저장 안됨");
        }
    }
}
