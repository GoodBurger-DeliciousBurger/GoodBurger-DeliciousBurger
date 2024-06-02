using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMain : MonoBehaviour
{
    // Image
    public Image characterImage; // ĳ���� �̹���
    public Image orderImage; // ���� �ֹ� �̹���

    // Text
    public Text orderText;  // UI Text ��Ҹ� �����ϱ� ���� ����
    public Text levelText;  // ������ ǥ���� UI Text ���

    // Button
    public Button yesBtn; // �ֹ� �� '��' ��ư
    public Button noBtn; // �ֹ� �� '�ƴϿ�' ��ư

    // ���� ����
    private int currentOrder = 1; // �ֹ���
    private int totalOrder = 8; // �ִ� �ֹ�
    private int currentLevel = 1; // ���� ����

    void Start()
    {
        characterImage.gameObject.SetActive(false); // ĳ���� �̹��� ��Ȱ��ȭ
        orderImage.gameObject.SetActive(false); // ���� �ֹ� �̹��� ��Ȱ��ȭ
        yesBtn.gameObject.SetActive(false); // '��' ��ư ��Ȱ��ȭ
        noBtn.gameObject.SetActive(false); // '�ƴϿ�' ��ư ��Ȱ��ȭ

/*        UpdateOrderText();
        UpdateLevelText();*/

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
            currentOrder = 1;
            currentLevel++;
            UpdateLevelText();
        }
        UpdateOrderText();

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

        yield return new WaitForSeconds(delay);

        // �̹����� ��ư���� �ٽ� Ȱ��ȭ
        characterImage.gameObject.SetActive(true);
        orderImage.gameObject.SetActive(true);
        yesBtn.gameObject.SetActive(true);
        noBtn.gameObject.SetActive(true);
    }
}
