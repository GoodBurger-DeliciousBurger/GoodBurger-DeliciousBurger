using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameHomeButtonScript : MonoBehaviour
{
    public Button homeButton;

   void Start()
    {
        if (homeButton != null)
        {
            homeButton.onClick.AddListener(OnSkipButtonClick);
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
