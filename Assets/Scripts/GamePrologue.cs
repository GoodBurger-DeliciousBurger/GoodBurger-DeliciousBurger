using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GamePrologue : MonoBehaviour
{
    public Image imageA;
    public Image imageB;
    public float delay = 1.5f; // 지연 시간 (1~2초 사이로 설정)

    // text
    public TMP_Text prologueText;
    string dialogue;

    void Start()
    {
        // 게임이 시작될 때 코루틴을 시작
        StartCoroutine(SwitchImageAfterDelay());

        // 프롤로그 - 1
        dialogue = "이번주 핫이슈는 이번에 신장 개업한 ‘좋은 햄버거 맛있는 햄버거’ 가게입니다 !";
        StartCoroutine(Typing(dialogue));
    }

    void Update()
    {
        
    }

    IEnumerator Typing(string talk)
    {
        // text null값으로 설정
        prologueText.text = null;
        for(int i = 0; i < talk.Length; i++) 
        {
            prologueText.text += talk[i];
            // 속도
            yield return new WaitForSeconds(0.05f);
        }
    }

    IEnumerator SwitchImageAfterDelay()
    {
        // 이미지 A 활성화
        imageA.gameObject.SetActive(true);
        imageB.gameObject.SetActive(false);

        // delay 시간만큼 대기
        yield return new WaitForSeconds(delay);

        // 이미지 B 활성화, 이미지 A 비활성화
        imageA.gameObject.SetActive(false);
        imageB.gameObject.SetActive(true);
    }
}
