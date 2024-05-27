using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GamePrologue : MonoBehaviour
{
    // Image
    public Image image;  // Image를 public으로 선언
    public Sprite[] sprites;  // public으로 sprite 배열 선언
    int imageIndex = 0; // sprite 배열의 순서, 첫 번째는 0부터 시작

    // Text
    public TMP_Text prologueText;
    string[] dialogues; // 대사 배열로 선언
    int dialogueIndex = 0; // 현재 대사 인덱스
    bool isTyping = false; // 대사 출력 중인지 확인하는 플래그

    void Start()
    {
        // 대사 배열 초기화
        dialogues = new string[]
        {
            "이번주 핫이슈는 이번에 신장 개업한 ‘좋은 햄버거 맛있는 햄버거’ 가게입니다 !",
            "건너편 가게인 ‘햄버거 업고 튀어’ 가게와 라이벌 구도가 되었죠 !",
            "과연 ‘좋은 햄버거 맛있는 햄버거’ 가게 ‘햄버거 업고 튀어’ 가게를 이길 수 있을지",
            "매우 기대되는 부분입니다! 이기는 가게 우리팀!"
        };

        // Image 컴포넌트가 할당되지 않은 경우 GetComponent를 통해 가져옴
        if (image == null)
        {
            image = GetComponent<Image>();
            if (image == null)
            {
                Debug.LogError("Image component is not assigned and could not be found on the GameObject.");
                return;
            }
        }

        // 첫 번째 대사 자동 출력
        StartCoroutine(ShowDialogue());
    }

    void Update()
    {
        // 화면 터치 입력을 감지
        if (Input.GetMouseButtonDown(0) && !isTyping && dialogueIndex < dialogues.Length)
        {
            StartCoroutine(ShowDialogue());
        }
    }

    // 텍스트 타이핑 효과
    IEnumerator Typing(string t)
    {
        // 대사 출력 중으로 설정
        isTyping = true;
        // text를 null 값으로 설정
        prologueText.text = null;
        for (int i = 0; i < t.Length; i++)
        {
            prologueText.text += t[i];
            // 속도
            yield return new WaitForSeconds(0.05f);
        }
        // 대사 출력 완료
        isTyping = false;
    }

    // 대사 출력 코루틴
    IEnumerator ShowDialogue()
    {
        // 현재 대사 출력
        yield return StartCoroutine(Typing(dialogues[dialogueIndex]));

        // 대사 인덱스가 1부터이면 이미지 변경
        if (dialogueIndex > 0 && imageIndex < sprites.Length)
        {
            image.sprite = sprites[imageIndex];
            imageIndex++;
        }

        // 대사 인덱스 증가
        dialogueIndex++;
    }
}