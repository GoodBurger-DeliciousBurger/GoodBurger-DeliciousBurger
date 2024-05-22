using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GamePrologue : MonoBehaviour
{
    Image image;
    public Sprite[] sprites; // �迭�� ����
    // text
    public TMP_Text prologueText;
    string[] dialogues;
    int dialogueIndex = 0; // ���� ��� �ε���

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
        if (Input.GetMouseButtonDown(0) && dialogueIndex > 0 && dialogueIndex < dialogues.Length)
        {
            StartCoroutine(ShowDialogue());
        }
    }

    // �ؽ�Ʈ Ÿ���� ȿ��
    IEnumerator Typing(string t)
    {
        // text null������ ����
        prologueText.text = null;
        for (int i = 0; i < t.Length; i++)
        {
            prologueText.text += t[i];
            // �ӵ�
            yield return new WaitForSeconds(0.05f);
        }
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
