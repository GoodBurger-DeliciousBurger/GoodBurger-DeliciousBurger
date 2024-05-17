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
    public float delay = 1.5f; // ���� �ð� (1~2�� ���̷� ����)

    // text
    public TMP_Text prologueText;
    string dialogue;

    void Start()
    {
        // ������ ���۵� �� �ڷ�ƾ�� ����
        StartCoroutine(SwitchImageAfterDelay());

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

    IEnumerator SwitchImageAfterDelay()
    {
        // �̹��� A Ȱ��ȭ
        imageA.gameObject.SetActive(true);
        imageB.gameObject.SetActive(false);

        // delay �ð���ŭ ���
        yield return new WaitForSeconds(delay);

        // �̹��� B Ȱ��ȭ, �̹��� A ��Ȱ��ȭ
        imageA.gameObject.SetActive(false);
        imageB.gameObject.SetActive(true);
    }
}
