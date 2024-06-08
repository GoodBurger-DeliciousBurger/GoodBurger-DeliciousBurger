using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReceiptDetails : MonoBehaviour
{
    public Button imageButton;
    public Image image1;
    public Image image2;
    public GameObject[] movableObjects;
    public Text orderMessageText; // 영수증에 표시할 주문 메시지 텍스트

    private static string orderMessage; // 주문 메시지를 저장할 정적 변수

    // 주문 메시지를 설정하는 메서드
    public static void SetOrderMessage(string message)
    {
        orderMessage = message;
    }

    // Start is called before the first frame update
    void Start()
    {
        imageButton.onClick.AddListener(OnButtonClick);

        image1.gameObject.SetActive(false);
        image2.gameObject.SetActive(false);
        orderMessageText.gameObject.SetActive(false); // 주문 메시지 텍스트 비활성화

        image1.raycastTarget = false;

        if (image2.GetComponent<Button>() == null)
        {
            image2.gameObject.AddComponent<Button>();
        }

        image2.GetComponent<Button>().onClick.AddListener(OnImageClick);
    }

    void OnButtonClick()
    {
        image1.gameObject.SetActive(true);
        image2.gameObject.SetActive(true);
        orderMessageText.gameObject.SetActive(true); // 주문 메시지 텍스트 활성화
        orderMessageText.text = orderMessage; // 주문 메시지 설정

        // 다른 오브젝트들의 상태 변경
        foreach (GameObject obj in movableObjects)
        {
            // 이동 가능하도록 콜라이더 비활성화
            Collider2D collider = obj.GetComponent<Collider2D>();
            if (collider != null)
            {
                collider.enabled = false;
            }
        }
    }

    void OnImageClick()
    {
        image1.gameObject.SetActive(false);
        image2.gameObject.SetActive(false);
        orderMessageText.gameObject.SetActive(false); // 주문 메시지 텍스트 비활성화

        // 다른 오브젝트들의 상태 변경
        foreach (GameObject obj in movableObjects)
        {
            // 이동 가능하도록 콜라이더 활성화
            Collider2D collider = obj.GetComponent<Collider2D>();
            if (collider != null)
            {
                collider.enabled = true;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
