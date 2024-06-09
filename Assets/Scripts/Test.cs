using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Test : MonoBehaviour
{
    public Text countdownText;  // Countdown�� ǥ���� Text
    private int countdownTime = 30;  // �ʱ� �ð�

    void Start()
    {
        if (countdownText != null)
        {
            StartCoroutine(StartCountdown());
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
    }
}
