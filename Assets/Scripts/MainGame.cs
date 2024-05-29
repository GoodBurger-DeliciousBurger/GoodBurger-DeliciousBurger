using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainGame : MonoBehaviour
{
    public Text progressText;  // UI Text ��Ҹ� �����ϱ� ���� ����
    private int currentProgress = 1; // �ֹ���
    private int totalProgress = 8; // �ִ� �ֹ�

    void Start()
    {
        UpdateProgressText();
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
        if (currentProgress < totalProgress)
        {
            Debug.Log("OnScreenTouch!");
            currentProgress++;
            UpdateProgressText();
        }
    }

    private void UpdateProgressText()
    {
        if (progressText != null)
        {
            progressText.text = currentProgress + "/" + totalProgress;
        }
        else
        {
            Debug.LogError("Progress Text is not assigned!");
        }
    }
}
