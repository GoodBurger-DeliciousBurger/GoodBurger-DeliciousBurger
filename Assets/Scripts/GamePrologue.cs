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
        // ���ѷα� - 1
        dialogue = "�̹��� ���̽��� �̹��� ���� ������ ������ �ܹ��� ���ִ� �ܹ��š� �����Դϴ� !";
        StartCoroutine(Typing(dialogue));
    }

    void Update()
    {
        
    }

    IEnumerator Typing(string talk)
    {
        // text null������ ����
        prologueText.text = null;
        for(int i = 0; i < talk.Length; i++) 
        {
            prologueText.text += talk[i];
            // �ӵ�
            yield return new WaitForSeconds(0.05f);
        }
    }
}
