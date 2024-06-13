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
        // ���� �� �̸� ����
        previousScene = SceneManager.GetActiveScene().name;
        PlayerPrefs.SetString(PreviousSceneKey, previousScene);

        // ���� �ð� ����
        if (previousScene == "MainGameScene")
        {
            PlayerPrefs.SetInt(SavedTimeKey, FindObjectOfType<Test>().GetSavedTime());
        }

        // ������ �� �ε�
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
            Debug.Log("���� �� �̸� ���� �ȵ�");
        }
    }
}
