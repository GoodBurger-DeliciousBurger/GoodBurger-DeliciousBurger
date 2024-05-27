using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SkipButtonScript : MonoBehaviour
{
    public Button skipButton;

   void Start()
    {
        if (skipButton != null)
        {
            skipButton.onClick.AddListener(OnSkipButtonClick);
        }
        else
        {
            Debug.LogError("Skip Button is not assigned in the Inspector!");
        }
    }

    void OnSkipButtonClick()
    {
        Debug.Log("Skip button clicked!");
        // TODO: 현재 메인씬이 없어서 다른 씬 넣어둠. 메인씬 생길 시 LoadScene 변경하기 
        SceneManager.LoadScene("MainScene");
    }
}
