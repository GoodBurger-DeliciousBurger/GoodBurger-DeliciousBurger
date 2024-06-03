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

        // 다른 오브젝트들의 상태 변경
        foreach (GameObject obj in movableObjects)
        {
            obj.SetActive(false); // 이동 불가능하도록 비활성화
        }
    }

    void OnImageClick()
    {
        image1.gameObject.SetActive(false);
        image2.gameObject.SetActive(false);

        // 다른 오브젝트들의 상태 변경
        foreach (GameObject obj in movableObjects)
        {
            obj.SetActive(true); // 이동 가능하도록 활성화
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
