using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoRecipeScene : MonoBehaviour
{

    private static string previousScene;
    public void GoRecipeBtn()
    {
        previousScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene("RecipeScene");
    }
    public void GoBackPreviousScene()
    {
        if (!string.IsNullOrEmpty(previousScene))
        {
            SceneManager.LoadScene(previousScene);
        }
        else
        {
            Debug.Log("¿Ã¿¸ æ¿ ¿Ã∏ß ¿˙¿Â æ»µ ");
        }
    }

}
