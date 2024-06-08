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

    // Start is called before the first frame update
    void Start()
    {
        imageButton.onClick.AddListener(OnButtonClick);

        image1.gameObject.SetActive(false);
        image2.gameObject.SetActive(false);

        image1.raycastTarget = false;

        if (image2.GetComponent<Button>() == null)
        {
            image2.gameObject.AddComponent<Button>();
        }

        image2.GetComponent<Button>().onClick.AddListener(OnImageClick);

    }

    void OnButtonClick()
    {
        image1.gameObject.SetActive (true);
        image2.gameObject.SetActive (true);

        // �ٸ� ������Ʈ���� ���� ����
        foreach (GameObject obj in movableObjects)
        {
            // �̵� �����ϵ��� �ݶ��̴� Ȱ��ȭ
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

        // �ٸ� ������Ʈ���� ���� ����
        foreach (GameObject obj in movableObjects)
        {
            // �̵� �����ϵ��� �ݶ��̴� Ȱ��ȭ
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
