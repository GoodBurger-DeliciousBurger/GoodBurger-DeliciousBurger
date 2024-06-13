using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class Test : MonoBehaviour
{
    public Text countdownText;  // Countdown을 표시할 Text
    public Button receiptBtn;   // 버튼을 참조하기 위한 변수
    private int countdownTime = 30;  // 초기 시간
    private const string SavedTimeKey = "SavedTime";  // PlayerPrefs 키

    void Start()
    {
        // 저장된 시간이 있는 경우 불러와서 countdownTime을 초기화
        if (PlayerPrefs.HasKey(SavedTimeKey))
        {
            countdownTime = PlayerPrefs.GetInt(SavedTimeKey);
        }

        if (countdownText != null)
        {
            StartCoroutine(StartCountdown());
        }

        if (receiptBtn != null)
        {
            receiptBtn.onClick.AddListener(SaveCurrentTime);
        }
    }

    // 30초 카운트다운 함수
    IEnumerator StartCountdown()
    {
        while (countdownTime > 0)
        {
            countdownText.text = countdownTime.ToString() + " sec";
            yield return new WaitForSeconds(1f);  // 1초 대기
            countdownTime--;  // 시간 감소
        }
        countdownText.text = "0 sec";  // 카운트다운이 끝나면 0 표시
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("GameScene");
    }

    // 버튼을 눌렀을 때 현재 시간을 저장하는 함수
    void SaveCurrentTime()
    {
        PlayerPrefs.SetInt(SavedTimeKey, countdownTime);
        Debug.Log("Saved Time: " + countdownTime + " sec");
    }

    public int GetSavedTime()
    {
        return countdownTime;
    }

    void OnApplicationQuit()
    {
        // 애플리케이션 종료 시 PlayerPrefs를 정리
        PlayerPrefs.DeleteKey(SavedTimeKey);
    }
}
