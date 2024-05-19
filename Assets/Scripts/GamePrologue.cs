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
        // ���ѷα� - 1
        dialogue = "�̹��� ���̽��� �̹��� ���� ������ ������ �ܹ��� ���ִ� �ܹ��š� �����Դϴ� !";
        StartCoroutine(ShowDialogues());
    }

    void Update()
    {
        
    }

    // �ؽ�Ʈ Ÿ���� ȿ��
    IEnumerator Typing(string t)
    {
        // text null������ ����
        prologueText.text = null;
        for(int i = 0; i < t.Length; i++)
        {
            prologueText.text += t[i];
            // �ӵ�
            yield return new WaitForSeconds(0.05f);
        }
    }

    // ���ѷα� ��� ��� �ڷ�ƾ
    IEnumerator ShowDialogues()
    {
        // ���ѷα� - 1
        yield return StartCoroutine(Typing(dialogue));
        // ���� ��� �ӵ�
        yield return new WaitForSeconds(2.0f);
        // ���ѷα� - 2
        yield return StartCoroutine(Typing("       �ǳ��� ������ ���ܹ��� ���� Ƣ� ���Կ� ���̹� ������ �Ǿ��� !"));
    }
}
