using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RetryButtonScript : MonoBehaviour
{
    public Button returyButton;

   void Start()
    {
        if (returyButton != null)
        {
            returyButton.onClick.AddListener(OnSkipButtonClick);
        }
        else
        {
            Debug.LogError("Retry Button is not assigned in the Inspector!");
        }
    }

    void OnSkipButtonClick()
    {
        Debug.Log("Retry button clicked!");
        SceneManager.LoadScene("MainScene");
    }
}
