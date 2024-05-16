using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GamePrologue : MonoBehaviour
{
    public TMP_Text prologueText;
    string dialogue;

    void Start()
    {
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
}
