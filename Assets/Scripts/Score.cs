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


    // ������ ���� �޴� üũ�ؼ� ������ �´��� �Ǻ�
    private void checkOrder()
    {
        //ġ�����
        if (receiptDetails.orderMessageText.Equals("������ �ּ��� !!") || receiptDetails.orderMessageText.Equals("������ �����Ѱ� ����׿� ġ�� ���� �ϳ���"))
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

   

}
