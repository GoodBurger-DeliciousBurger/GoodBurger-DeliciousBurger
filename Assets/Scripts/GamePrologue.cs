using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GamePrologue : MonoBehaviour
{
    // Image
    public Image image;  // Image�� public���� ����
    public Sprite[] sprites;  // public���� sprite �迭 ����
    int imageIndex = 0; // sprite �迭�� ����, ù ��°�� 0���� ����

    // Text
    public TMP_Text prologueText;
    string[] dialogues; // ��� �迭�� ����
    int dialogueIndex = 0; // ���� ��� �ε���
    bool isTyping = false; // ��� ��� ������ Ȯ���ϴ� �÷���

    // ��ġ ���� �� �ȳ� �ؽ�Ʈ
    public TMP_Text touchToScreenText; // "Touch to Screen!" �ؽ�Ʈ
    private float touchTimer = 0f; // ��ġ Ÿ�̸�
    private const float touchThreshold = 5f; // ��ġ ���� �ð�

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

        // Image ������Ʈ�� �Ҵ���� ���� ��� GetComponent�� ���� ������
        if (image == null)
        {
            image = GetComponent<Image>();
            if (image == null)
            {
                Debug.LogError("Image component is not assigned and could not be found on the GameObject.");
                return;
            }
        }

        // ù ��° ��� �ڵ� ���
        StartCoroutine(ShowDialogue());

        // "Touch to Screen!" �ؽ�Ʈ ��Ȱ��ȭ
        if (touchToScreenText != null)
        {
            touchToScreenText.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        // ȭ�� ��ġ �Է��� ����
        if (Input.GetMouseButtonDown(0) && !isTyping && dialogueIndex < dialogues.Length)
        {
            touchTimer = 0f; // ��ġ�� �����Ǹ� Ÿ�̸Ӹ� �ʱ�ȭ
            if (touchToScreenText != null)
            {
                touchToScreenText.gameObject.SetActive(false); // �ؽ�Ʈ ��Ȱ��ȭ
            }
            StartCoroutine(ShowDialogue());
        }
        else
        {
            touchTimer += Time.deltaTime; // ��ġ�� ������ Ÿ�̸� ����
            if (touchTimer >= touchThreshold)
            {
                if (touchToScreenText != null)
                {
                    touchToScreenText.gameObject.SetActive(true); // 5�� �̻� ��ġ�� ������ �ؽ�Ʈ Ȱ��ȭ
                }
            }
        }
    }

    // �ؽ�Ʈ Ÿ���� ȿ��
    IEnumerator Typing(string t)
    {
        // ��� ��� ������ ����
        isTyping = true;
        // text�� null ������ ����
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

        // ��� �ε����� 1�����̸� �̹��� ����
        if (dialogueIndex > 0 && imageIndex < sprites.Length)
        {
            image.sprite = sprites[imageIndex];
            imageIndex++;
        }

        // ��� �ε��� ����
        dialogueIndex++;

        // ������ ��簡 ���� �� GameScene���� �̵�
        if (dialogueIndex >= dialogues.Length)
        {
            yield return new WaitForSeconds(1.0f);
            SceneManager.LoadScene("GameScene");
        }
    }
}
