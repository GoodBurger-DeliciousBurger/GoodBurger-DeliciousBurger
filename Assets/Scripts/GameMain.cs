using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMain : MonoBehaviour
{
    public Text orderText;  // UI Text 요소를 연결하기 위한 변수
    public Text levelText;  // 레벨을 표시할 UI Text 요소 추가
    private int currentOrder = 1; // 주문수
    private int totalOrder = 8; // 최대 주문
    private int currentLevel = 1; // 현재 레벨

    void Start()
    {
        UpdateOrderText();
        UpdateLevelText();
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
        if (currentOrder < totalOrder)
        {
            currentOrder++;
        }
        else
        {
            currentOrder = 1;
            currentLevel++;
            UpdateLevelText();
        }
        UpdateOrderText();
    }

    private void UpdateOrderText()
    {
        if (orderText != null)
        {
            orderText.text = currentOrder + "/" + totalOrder;
        }
        else
        {
            Debug.LogError("Order Text is not assigned!");
        }
    }

    private void UpdateLevelText()
    {
        if (levelText != null)
        {
            levelText.text = "Lv " + currentLevel;
        }
        else
        {
            Debug.LogError("Level Text is not assigned!");
        }
    }
}
