using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class Test : MonoBehaviour
{
    public Text countdownText;  // Countdown�� ǥ���� Text
    public Button receiptBtn;   // ��ư�� �����ϱ� ���� ����
    private int countdownTime = 30;  // �ʱ� �ð�
    private const string SavedTimeKey = "SavedTime";  // PlayerPrefs Ű

    void Start()
    {
        // ����� �ð��� �ִ� ��� �ҷ��ͼ� countdownTime�� �ʱ�ȭ
        if (PlayerPrefs.HasKey(SavedTimeKey))
        {
            countdownTime = PlayerPrefs.GetInt(SavedTimeKey);
        }

        if (countdownText != null)
        {
            StartCoroutine(StartCountdown());
        }

        if (receiptBtn != null)
        {
            receiptBtn.onClick.AddListener(SaveCurrentTime);
        }
    }

    // 30�� ī��Ʈ�ٿ� �Լ�
    IEnumerator StartCountdown()
    {
        while (countdownTime > 0)
        {
            countdownText.text = countdownTime.ToString() + " sec";
            yield return new WaitForSeconds(1f);  // 1�� ���
            countdownTime--;  // �ð� ����
        }
        countdownText.text = "0 sec";  // ī��Ʈ�ٿ��� ������ 0 ǥ��
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("GameScene");
    }

    // ��ư�� ������ �� ���� �ð��� �����ϴ� �Լ�
    void SaveCurrentTime()
    {
        PlayerPrefs.SetInt(SavedTimeKey, countdownTime);
        Debug.Log("Saved Time: " + countdownTime + " sec");
    }

    public int GetSavedTime()
    {
        return countdownTime;
    }

    void OnApplicationQuit()
    {
        // ���ø����̼� ���� �� PlayerPrefs�� ����
        PlayerPrefs.DeleteKey(SavedTimeKey);
    }
}
