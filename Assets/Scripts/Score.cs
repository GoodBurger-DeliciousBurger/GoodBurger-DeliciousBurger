using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    public static Score Instance { get; private set; }
    
    private ReceiptDetails receiptDetails;

    public int persent = 90;

    void UpdatePersent()
    {
        persent = 90;
        GameMain.SetPersent(persent);
    }


    // 레시피 별로 메뉴 체크해서 순서가 맞는지 판별
    private void checkOrder()
    {
        //치즈버거
        if (receiptDetails.orderMessageText.Equals("띠드버거 주세욤 !!") || receiptDetails.orderMessageText.Equals("오늘은 느끼한게 땡기네요 치즈 버거 하나요"))
        {

        }

        //새우버거
        if (receiptDetails.orderMessageText.Equals("새우가 드라마를 찍으면? 대하드라마 !! " + "하하 !! 새우 버거 하나 주세요 !"))
        {

        }

        //핫크리스피 버거
        if (receiptDetails.orderMessageText.Equals("아주 매운 햄버거 주세요 !") || receiptDetails.orderMessageText.Equals("치킨 버거 주세요 !!"))
        {

        }

        //더블패티버거
        if (receiptDetails.orderMessageText.Equals("패티가 따블 !! 더블패티 하나요 !"))
        {

        }

        //불고기 버거
        if (receiptDetails.orderMessageText.Equals("오늘은... 불고기 ! 불고기 버거 하나 부탁드려요 !") || receiptDetails.orderMessageText.Equals("기본 하나요 ! 데리버거인가?"))
        {

        }
    }

   

}
