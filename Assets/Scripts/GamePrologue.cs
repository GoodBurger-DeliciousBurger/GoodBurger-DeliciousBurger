using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GamePrologue : MonoBehaviour
{
    public TMP_Text prologueText;
    string[] dialogues;
    int dialogueIndex = 0; // ���� ��� �ε���
    bool isTyping = false; // ��� ��� ������ Ȯ���ϴ� �÷���

    void Start()
    {
        // ��� �迭 �ʱ�ȭ
        dialogues = new string[]
        {
            "�̹��� ���̽��� �̹��� ���� ������ ������ �ܹ��� ���ִ� �ܹ��š� �����Դϴ� !",
            "�ǳ��� ������ ���ܹ��� ���� Ƣ� ���Կ� ���̹� ������ �Ǿ��� !",
            "���� ������ �ܹ��� ���ִ� �ܹ��š� ���� ���ܹ��� ���� Ƣ� ���Ը� �̱� �� ������",
            "�ſ� ���Ǵ� �κ��Դϴ�! �̱�� ���� �츮��!"
        };

        // ù ��° ��� �ڵ� ���
        StartCoroutine(ShowDialogue());
    }

    void Update()
    {
        // ��ġ �Է��� ����
        if (Input.GetMouseButtonDown(0) && !isTyping && dialogueIndex < dialogues.Length)
        {
            StartCoroutine(ShowDialogue());
        }
    }

    // �ؽ�Ʈ Ÿ���� ȿ��
    IEnumerator Typing(string t)
    {
        // ��� ��� ������ ����
        isTyping = true;
        // text null������ ����
        prologueText.text = null;
        for (int i = 0; i < t.Length; i++)
        {
            prologueText.text += t[i];
            // �ӵ�
            yield return new WaitForSeconds(0.05f);
        }
        // ��� ��� �Ϸ�
        isTyping = false;
    }

    // ��� ��� �ڷ�ƾ
    IEnumerator ShowDialogue()
    {
        // ���� ��� ���
        yield return StartCoroutine(Typing(dialogues[dialogueIndex]));
        // ��� �ε��� ����
        dialogueIndex++;
        // ���� ��� �ӵ�
        yield return new WaitForSeconds(2.0f);
    }
}
