using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    public static Score Instance { get; private set; }

    private static ReceiptDetails receiptDetails;

    public static int persent = 0;

    void UpdatePersent()
    {
        persent = 90;
        GameMain.SetPersent(persent);
    }


    // �ð��ȿ� ������°� üũ
    /*private int InTime(int persent)
    {
        if () {  // �ð��ȿ� ������ ���߳�
     
        }
        else // �ð��ȿ� ����
        {

        }
    }*/


}
