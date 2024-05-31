using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMain : MonoBehaviour
{
    public Text orderText;  // UI Text ��Ҹ� �����ϱ� ���� ����
    public Text levelText;  // ������ ǥ���� UI Text ��� �߰�
    private int currentOrder = 1; // �ֹ���
    private int totalOrder = 8; // �ִ� �ֹ�
    private int currentLevel = 1; // ���� ����

    void Start()
    {
        UpdateOrderText();
        UpdateLevelText();
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
    // �ֹ� �� ���� 
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
}
