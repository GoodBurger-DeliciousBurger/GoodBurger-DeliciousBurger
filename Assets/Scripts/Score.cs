using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    private ReceiptDetails receiptDetails;
    private GameMain gameMain;

    // ������ ���� �޴� üũ�ؼ� ������ �´��� �Ǻ�
    private void checkOrder()
    {
        //ġ�����
        if(receiptDetails.orderMessageText.Equals("������ �ּ��� !!") || receiptDetails.orderMessageText.Equals("������ �����Ѱ� ����׿� ġ�� ���� �ϳ���"))
        {

        }

        //�������
        if (receiptDetails.orderMessageText.Equals("���찡 ��󸶸� ������? ���ϵ�� !! " + "���� !! ���� ���� �ϳ� �ּ��� !"))
        {

        }

        //��ũ������ ����
        if (receiptDetails.orderMessageText.Equals("���� �ſ� �ܹ��� �ּ��� !") || receiptDetails.orderMessageText.Equals("ġŲ ���� �ּ��� !!"))
        {

        }

        //������Ƽ����
        if (receiptDetails.orderMessageText.Equals("��Ƽ�� ���� !! ������Ƽ �ϳ��� !"))
        {

        }

        //�Ұ�� ����
        if (receiptDetails.orderMessageText.Equals("������... �Ұ�� ! �Ұ�� ���� �ϳ� ��Ź����� !") || receiptDetails.orderMessageText.Equals("�⺻ �ϳ��� ! ���������ΰ�?"))
        {

        }
    }


    // ������ üũ
    public void lvUp()
    {
        //if(gameMain.percent==100) gameMain.currentLevel = 2;
        //return gameMain.currentLevel;
    }

}
