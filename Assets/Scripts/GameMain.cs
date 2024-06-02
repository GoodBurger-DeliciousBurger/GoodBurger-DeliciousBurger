using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMain : MonoBehaviour
{
    // Image
    public Image characterImage; // 캐릭터 이미지
    public Image orderImage; // 음식 주문 이미지

    // Text
    public Text orderText;  // UI Text 요소를 연결하기 위한 변수
    public Text levelText;  // 레벨을 표시할 UI Text 요소

    // Button
    public Button yesBtn;

    // 변수 선언
    private int currentOrder = 1; // 주문수
    private int totalOrder = 8; // 최대 주문
    private int currentLevel = 1; // 현재 레벨

    void Start()
    {
        characterImage.gameObject.SetActive(false); // 캐릭터 이미지 비활성화
        orderImage.gameObject.SetActive(false); // 음식 주문 이미지 비활성화
        yesBtn.gameObject.SetActive(false); // '네' 버튼 비활성화

        UpdateOrderText();
        UpdateLevelText();

        StartCoroutine(ShowCharacterImageAfterDelay(2.5f));

        yesBtn.onClick.AddListener(OnYesButtonClick);
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
    // 화면 터치 시 주문 수 증가 
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
    
    // 주문 수 증가 (text)
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

    // 레벨 증가 (text)
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

    // 고객 이미지 n초 후 나타남
    private IEnumerator ShowCharacterImageAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (characterImage != null)
        {
            characterImage.gameObject.SetActive(true); // 캐릭터 이미지를 활성화
            orderImage.gameObject.SetActive(true); // 음식 주문 이미지 비활성화
            yesBtn.gameObject.SetActive(true); // '네' 버튼 활성화
        }
        else
        {
            Debug.LogError("Character Image is not assigned!");
        }
    }

    // 주문 시 '네' 버튼 
    void OnYesButtonClick()
    {
        SceneManager.LoadScene("MainGameScene");
    }
}
