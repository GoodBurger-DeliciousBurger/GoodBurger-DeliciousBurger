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


    // 시간안에 만들었는가 체크
    /*private int InTime(int persent)
    {
        if () {  // 시간안에 만들지 못했나
     
        }
        else // 시간안에 만듬
        {

        }
    }*/


}
