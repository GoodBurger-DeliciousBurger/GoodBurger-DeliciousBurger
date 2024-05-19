using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GamePrologue : MonoBehaviour
{
    // text
    public TMP_Text prologueText;
    string dialogue;

    void Start()
    {
        // 프롤로그 - 1
        dialogue = "이번주 핫이슈는 이번에 신장 개업한 ‘좋은 햄버거 맛있는 햄버거’ 가게입니다 !";
        StartCoroutine(ShowDialogues());
    }

    void Update()
    {
        
    }

    // 텍스트 타이핑 효과
    IEnumerator Typing(string t)
    {
        // text null값으로 설정
        prologueText.text = null;
        for(int i = 0; i < t.Length; i++)
        {
            prologueText.text += t[i];
            // 속도
            yield return new WaitForSeconds(0.05f);
        }
    }

    // 프롤로그 대사 출력 코루틴
    IEnumerator ShowDialogues()
    {
        // 프롤로그 - 1
        yield return StartCoroutine(Typing(dialogue));
        // 다음 대사 속도
        yield return new WaitForSeconds(2.0f);
        // 프롤로그 - 2
        yield return StartCoroutine(Typing("       건너편 가게인 ‘햄버거 업고 튀어’ 가게와 라이벌 구도가 되었죠 !"));
    }
}
