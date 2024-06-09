using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Test : MonoBehaviour
{
    public Text countdownText;  // Countdown을 표시할 Text
    private int countdownTime = 30;  // 초기 시간

    void Start()
    {
        if (countdownText != null)
        {
            StartCoroutine(StartCountdown());
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
    }
}
