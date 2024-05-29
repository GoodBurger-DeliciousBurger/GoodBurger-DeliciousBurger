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
        SceneManager.LoadScene("MainGameScene");
    }
}
