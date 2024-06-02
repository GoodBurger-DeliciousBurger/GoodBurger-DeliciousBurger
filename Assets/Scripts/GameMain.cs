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
    public Button yesBtn;

    // ���� ����
    private int currentOrder = 1; // �ֹ���
    private int totalOrder = 8; // �ִ� �ֹ�
    private int currentLevel = 1; // ���� ����

    void Start()
    {
        characterImage.gameObject.SetActive(false); // ĳ���� �̹��� ��Ȱ��ȭ
        orderImage.gameObject.SetActive(false); // ���� �ֹ� �̹��� ��Ȱ��ȭ
        yesBtn.gameObject.SetActive(false); // '��' ��ư ��Ȱ��ȭ

        UpdateOrderText();
        UpdateLevelText();

        StartCoroutine(ShowCharacterImageAfterDelay(2.5f));

        yesBtn.onClick.AddListener(OnYesButtonClick);
    }

    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            OnScreenTouch();
        }
        else if (Input.GetMouseButtonDown(0))  // ���콺 Ŭ���� ����
        {
            OnScreenTouch();
        }
    }

    // TODO: '��' ��ư ���� �� ȭ�� ��ġ�ϸ� �ֹ� �������, �� ��ư ���� �� �ֹ� �� ������ ����
    // ȭ�� ��ġ �� �ֹ� �� ���� 
    private void OnScreenTouch()
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
            orderImage.gameObject.SetActive(true); // ���� �ֹ� �̹��� ��Ȱ��ȭ
            yesBtn.gameObject.SetActive(true); // '��' ��ư Ȱ��ȭ
        }
        else
        {
            Debug.LogError("Character Image is not assigned!");
        }
    }

    // �ֹ� �� '��' ��ư 
    void OnYesButtonClick()
    {
        SceneManager.LoadScene("MainGameScene");
    }
}
