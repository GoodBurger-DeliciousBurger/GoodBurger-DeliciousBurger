using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMain : MonoBehaviour
{
    //����
    public static Score score;

    // Image
    public Image characterImage; // ĳ���� �̹���
    public Image orderImage; // ���� �ֹ� �̹���

    // Text
    public Text orderText;  // UI Text ��Ҹ� �����ϱ� ���� ����
    public Text levelText;  // ������ ǥ���� UI Text ���
    public Text orderMessageText; // �ֹ� �޼��� �ؽ�Ʈ
    public Text persentText; // �ۼ�Ʈ �ؽ�Ʈ

    // Button
    public Button yesBtn; // �ֹ� �� '��' ��ư
    public Button noBtn; // �ֹ� �� '�ƴϿ�' ��ư

    // ���� ����
    private static int currentOrder = 0; // �ֹ���
    private static int currentLevel = 1; // ���� ����
    private int totalOrder = 10; // �ִ� �ֹ�
    private static int updatePersent; // �ۼ�Ʈ

    // ���� �ֹ� �޽��� �迭
    private string[] messages =
        {
        "������ �ּ��� !!", // ġ�� ����
        "������ �����Ѱ� ����׿� ġ�� ���� �ϳ���", // ġ�� ���� ���� 
        "���찡 ��󸶸� ������? ���ϵ�� !! " + "���� !! ���� ���� �ϳ� �ּ��� !", // ���� ����
        "���� �ſ� �ܹ��� �ּ��� !", // ��ũ������ ����
        "ġŲ ���� �ּ��� !!", // ��ũ������ ���� ����
        "��Ƽ�� ���� !! ������Ƽ �ϳ��� !", // ������Ƽ ����
        "������... �Ұ�� ! �Ұ�� ���� �ϳ� ��Ź����� !", // �Ұ�� ���� 
        "�⺻ �ϳ��� ! ���������ΰ�?" // �Ұ�� ���� ����
    };

    // ĳ���� �̹��� �迭
    public Sprite[] characterSprites; // ���� ĳ���� �̹������� ������ �迭

    void Start()
    {
        score = GetComponent<Score>();

        // ���� ó�� ���� �Ǵ� ����� �� �ֹ� �� �ʱ�ȭ
        if (PlayerPrefs.GetInt("GameStarted", 0) == 0)
        {
            currentOrder = 0;
            currentLevel = 1;
            PlayerPrefs.SetInt("GameStarted", 1);
        }

        UpdateOrderText();
        UpdateLevelText();
        UpdatePersentText();

        characterImage.gameObject.SetActive(false); // ĳ���� �̹��� ��Ȱ��ȭ
        orderImage.gameObject.SetActive(false); // ���� �ֹ� �̹��� ��Ȱ��ȭ
        yesBtn.gameObject.SetActive(false); // '��' ��ư ��Ȱ��ȭ
        noBtn.gameObject.SetActive(false); // '�ƴϿ�' ��ư ��Ȱ��ȭ
        orderMessageText.gameObject.SetActive(false); // �ֹ� �޼��� �ؽ�Ʈ ��Ȱ��ȭ

        // '�ƴϿ�' ��ư ���� �� �ٽ� �ֹ� �� �� �ִ� �ڷ�ƾ
        StartCoroutine(ShowCharacterImageAfterDelay(2.5f));

        yesBtn.onClick.AddListener(OnYesButtonClick);
        noBtn.onClick.AddListener(OnNoButtonClick);
    }

    void Update()
    {

    }

    // �ֹ� �� ���� (text)
    private void UpdateOrderText()
    {
        if (orderText != null)
        {
            orderText.text = currentOrder + "/" + totalOrder;
        }
        else
        {
            Debug.LogError("Order Text is not assigned!");
        }
    }

    // ���� ���� (text)
    private void UpdateLevelText()
    {
        if (levelText != null)
        {
            levelText.text = "Lv " + currentLevel;
        }
        else
        {
            Debug.LogError("Level Text is not assigned!");
        }
    }

    // �ۼ�Ʈ �޾ƿ���
    public static void SetPersent(int persent)
    {
        updatePersent = persent;
        Debug.Log(persent);
    }

    // �ۼ�Ʈ ��� (text)
    public void UpdatePersentText()
    {
        if (persentText != null)
        {
            persentText.text = updatePersent + "%";
        }
        else
        {
            Debug.LogError("persent Text is not assigned!");
        }
    }

    // �� �̹��� n�� �� ��Ÿ��
    private IEnumerator ShowCharacterImageAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (characterImage != null)
        {
            characterImage.gameObject.SetActive(true); // ĳ���� �̹����� Ȱ��ȭ
            orderImage.gameObject.SetActive(true); // ���� �ֹ� �̹��� Ȱ��ȭ
            yesBtn.gameObject.SetActive(true); // '��' ��ư Ȱ��ȭ
            noBtn.gameObject.SetActive(true); // '�ƴϿ�' ��ư Ȱ��ȭ
            orderMessageText.gameObject.SetActive(true); // �ֹ� �޽��� �ؽ�Ʈ Ȱ��ȭ
            UpdateOrderMessageText(); // ���� �޽��� ����
        }
        else
        {
            Debug.LogError("Character Image is not assigned!");
        }
    }

    // �ֹ� �� '��' ��ư 
    void OnYesButtonClick()
    {
        if (currentOrder < totalOrder)
        {
            currentOrder++;
        }
        else
        {
            currentOrder = 0;
            currentLevel++;
            UpdateLevelText();
        }
        UpdateOrderText();

        // �������� �ֹ� �޽��� ����
        ReceiptDetails.SetOrderMessage(orderMessageText.text);

        // '��' ��ư ���� �� �ֹ���� �̵�
        SceneManager.LoadScene("MainGameScene");
    }

    // �ֹ� �� '�ƴϿ�' ��ư
    void OnNoButtonClick()
    {
        StartCoroutine(HideAndShowImagesAfterDelay(2.5f));
    }

    // '�ƴϿ�' ��ư ���� ��
    private IEnumerator HideAndShowImagesAfterDelay(float delay)
    {
        // �̹����� ��ư���� ��Ȱ��ȭ
        characterImage.gameObject.SetActive(false);
        orderImage.gameObject.SetActive(false);
        yesBtn.gameObject.SetActive(false);
        noBtn.gameObject.SetActive(false);
        orderMessageText.gameObject.SetActive(false);

        yield return new WaitForSeconds(delay);

        ShowRandomCharacterImage();
    }

    // �������� �ֹ� �޽��� ����
    private void UpdateOrderMessageText()
    {
        if (orderMessageText != null)
        {
            orderMessageText.text = messages[Random.Range(0, messages.Length)];
        }
        else
        {
            Debug.LogError("Order Message Text is not assigned!");
        }
    }

    // �������� ĳ���� �̹��� ����
    private void ShowRandomCharacterImage()
    {
        if (characterSprites != null && characterSprites.Length > 0)
        {
            characterImage.sprite = characterSprites[Random.Range(0, characterSprites.Length)]; // ĳ���� �̹��� �������� ����
            characterImage.gameObject.SetActive(true); // ĳ���� �̹����� Ȱ��ȭ
            orderImage.gameObject.SetActive(true); // ���� �ֹ� �̹��� Ȱ��ȭ
            yesBtn.gameObject.SetActive(true); // '��' ��ư Ȱ��ȭ
            noBtn.gameObject.SetActive(true); // '�ƴϿ�' ��ư Ȱ��ȭ
            orderMessageText.gameObject.SetActive(true); // �ֹ� �޽��� �ؽ�Ʈ Ȱ��ȭ

            UpdateOrderMessageText(); // ���� �޽��� ����
        }
        else
        {
            Debug.LogError("Character Sprites array is not assigned or empty!");
        }
    }

    void OnApplicationQuit()
    {
        // ���� ���� �� ���� ���� �ʱ�ȭ
        PlayerPrefs.SetInt("GameStarted", 0);
    }
}