using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainGame : MonoBehaviour
{
    public Text progressText;  // UI Text 요소를 연결하기 위한 변수
    private int currentProgress = 1; // 주문수
    private int totalProgress = 8; // 최대 주문

    void Start()
    {
        UpdateProgressText();
    }

    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            OnScreenTouch();
        }
        else if (Input.GetMouseButtonDown(0))  // 마우스 클릭도 감지
        {
            OnScreenTouch();
        }
    }

    // TODO: '네' 버튼 구현 시 화면 터치하면 주문 증가대신, 네 버튼 누를 시 주문 수 증가로 변경
    // 주문 수 증가 
    private void OnScreenTouch()
    {
        if (currentProgress < totalProgress)
        {
            Debug.Log("OnScreenTouch!");
            currentProgress++;
            UpdateProgressText();
        }
    }

    private void UpdateProgressText()
    {
        if (progressText != null)
        {
            progressText.text = currentProgress + "/" + totalProgress;
        }
        else
        {
            Debug.LogError("Progress Text is not assigned!");
        }
    }
}
