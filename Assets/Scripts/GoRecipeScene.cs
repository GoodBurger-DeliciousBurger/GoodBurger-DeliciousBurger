using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoRecipeScene : MonoBehaviour
{
    public void GoRecipeBtn()
    {
        SceneManager.LoadScene("RecipeScene");
        Debug.Log("·¹½ÃÇÇ");
    }
}
