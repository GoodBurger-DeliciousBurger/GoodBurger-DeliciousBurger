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
    public Text orderMessageText; // 주문 메세지 텍스트
    public Text persentText; // 퍼센트 텍스트

    // Button
    public Button yesBtn; // 주문 시 '네' 버튼
    public Button noBtn; // 주문 시 '아니요' 버튼

    // 변수 선언
    private static int currentOrder = 0; // 주문수
    private static int currentLevel = 1; // 현재 레벨
    private int totalOrder = 10; // 최대 주문
    private static int updatePersent = 0; // 퍼센트

    public static int persent = 0;

    // 랜덤 주문 메시지 배열
    private string[] messages =
        {
        "띠드버거 주세욤 !!", // 치즈 버거
        "오늘은 느끼한게 땡기네요 치즈 버거 하나요", // 치즈 버거 번외 
        "새우가 드라마를 찍으면? 대하드라마 !! " + "하하 !! 새우 버거 하나 주세요 !", // 새우 버거
        "아주 매운 햄버거 주세요 !", // 핫크리스피 버거
        "치킨 버거 주세요 !!", // 핫크리스피 버거 번외
        "패티가 따블 !! 더블패티 하나요 !", // 더블패티 버거
        "오늘은... 불고기 ! 불고기 버거 하나 부탁드려요 !", // 불고기 버거 
        "기본 하나요 ! 데리버거인가?" // 불고기 버거 번외
    };

    // 캐릭터 이미지 배열
    public Sprite[] characterSprites; // 여러 캐릭터 이미지들을 저장할 배열

    void Start()
    {

        // 게임 처음 시작 또는 재시작 시 주문 수 초기화
        if (PlayerPrefs.GetInt("GameStarted", 0) == 0)
        {
            currentOrder = 0;
            currentLevel = 1;
            updatePersent = 0;
            PlayerPrefs.SetInt("GameStarted", 1);
        }

        UpdateOrderText();
        UpdateLevelText();
        UpdatePersentText();

        characterImage.gameObject.SetActive(false); // 캐릭터 이미지 비활성화
        orderImage.gameObject.SetActive(false); // 음식 주문 이미지 비활성화
        yesBtn.gameObject.SetActive(false); // '네' 버튼 비활성화
        noBtn.gameObject.SetActive(false); // '아니요' 버튼 비활성화
        orderMessageText.gameObject.SetActive(false); // 주문 메세지 텍스트 비활성화

        // '아니요' 버튼 누를 시 다시 주문 할 수 있는 코루틴
        StartCoroutine(ShowCharacterImageAfterDelay(1f));

        yesBtn.onClick.AddListener(OnYesButtonClick);
        noBtn.onClick.AddListener(OnNoButtonClick);
    }

    void Update()
    {
        UpdatePersentText();
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

            // 레벨2가 되면 퍼센트 초기화
            if(currentLevel == 2)
            {
                updatePersent = 0;
            }
            
        }
        else
        {
            Debug.LogError("Level Text is not assigned!");
        }
    }

    // 퍼센트 받아오기
    public static void SetPersent(int persent)
    {
        updatePersent += persent;
        Debug.Log(updatePersent);
    }

    // 퍼센트 출력 (text)
    public void UpdatePersentText()
    {
        if (persentText != null)
        {
            persentText.text = updatePersent + "%";

            // 레벨업이 될 점수
            if (updatePersent >= 30)
            {
                currentLevel = 2;
                UpdateLevelText();
            }

            if(currentOrder==10 && updatePersent < 80)
            {
                SceneManager.LoadScene("Level1EndingScene");
            }
        }
        else
        {
            Debug.LogError("persent Text is not assigned!");
        }
    }

    // 고객 이미지 n초 후 나타남
    private IEnumerator ShowCharacterImageAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (characterImage != null)
        {
            characterImage.gameObject.SetActive(true); // 캐릭터 이미지를 활성화
            orderImage.gameObject.SetActive(true); // 음식 주문 이미지 활성화
            yesBtn.gameObject.SetActive(true); // '네' 버튼 활성화
            noBtn.gameObject.SetActive(true); // '아니요' 버튼 활성화
            orderMessageText.gameObject.SetActive(true); // 주문 메시지 텍스트 활성화
            UpdateOrderMessageText(); // 랜덤 메시지 설정
        }
        else
        {
            Debug.LogError("Character Image is not assigned!");
        }
    }

    // 주문 시 '네' 버튼 
    void OnYesButtonClick()
    {
        if (currentOrder < totalOrder)
        {
            currentOrder++;
        }
        else
        {
            currentOrder = 0;
            currentLevel++;
            //UpdateLevelText();
        }
        UpdateOrderText();

        // 영수증에 주문 메시지 설정
        ReceiptDetails.SetOrderMessage(orderMessageText.text);
        // 점수 판별을 위한 주문 설정
        Drag.SetOrderMessage(orderMessageText.text);

        // '네' 버튼 누를 시 주문대로 이동
        SceneManager.LoadScene("MainGameScene");
    }

    // 주문 시 '아니요' 버튼
    void OnNoButtonClick()
    {
        StartCoroutine(HideAndShowImagesAfterDelay(1f));
    }

    // '아니요' 버튼 누를 시
    private IEnumerator HideAndShowImagesAfterDelay(float delay)
    {
        // 이미지와 버튼들을 비활성화
        characterImage.gameObject.SetActive(false);
        orderImage.gameObject.SetActive(false);
        yesBtn.gameObject.SetActive(false);
        noBtn.gameObject.SetActive(false);
        orderMessageText.gameObject.SetActive(false);

        yield return new WaitForSeconds(delay);

        ShowRandomCharacterImage();
    }

    // 랜덤으로 주문 메시지 설정
    private void UpdateOrderMessageText()
    {
        if (orderMessageText != null)
        {
            orderMessageText.text = messages[Random.Range(0, messages.Length)];
        }
        else
        {
            Debug.LogError("Order Message Text is not assigned!");
        }
    }

    // 랜덤으로 캐릭터 이미지 설정
    private void ShowRandomCharacterImage()
    {
        if (characterSprites != null && characterSprites.Length > 0)
        {
            characterImage.sprite = characterSprites[Random.Range(0, characterSprites.Length)]; // 캐릭터 이미지 랜덤으로 설정
            characterImage.gameObject.SetActive(true); // 캐릭터 이미지를 활성화
            orderImage.gameObject.SetActive(true); // 음식 주문 이미지 활성화
            yesBtn.gameObject.SetActive(true); // '네' 버튼 활성화
            noBtn.gameObject.SetActive(true); // '아니요' 버튼 활성화
            orderMessageText.gameObject.SetActive(true); // 주문 메시지 텍스트 활성화

            UpdateOrderMessageText(); // 랜덤 메시지 설정
        }
        else
        {
            Debug.LogError("Character Sprites array is not assigned or empty!");
        }
    }

    void OnApplicationQuit()
    {
        // 게임 종료 시 게임 상태 초기화
        PlayerPrefs.SetInt("GameStarted", 0);
    }
}