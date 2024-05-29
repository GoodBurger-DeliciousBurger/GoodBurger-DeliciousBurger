using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragBackground : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public float sensitivity = 10.0f; // ���콺 ����
    public float moveSpeed = 10.0f;   // ī�޶� �̵� �ӵ�
    public float minX = 792.0f;       // ī�޶� �̵� �ּ� x ��ǥ
    public float maxX = 1146.0f;        // ī�޶� �̵� �ִ� x ��ǥ

    private void Update()
    {

        // ���콺 ��ġ�� ȭ���� �¿� ���� ��Ҵ��� Ȯ��
        if (Input.mousePosition.x <= 0)
        {
            MoveCameraLeft();
        }
        else if (Input.mousePosition.x >= Screen.width)
        {
            MoveCameraRight();
        }
       
    }
    private void MoveCameraLeft()
    {
        // �������� �̵�
        transform.Translate(Vector3.left * sensitivity * Time.deltaTime * moveSpeed);

        // ī�޶� �̵� ���� ����
        Vector3 clampedPosition = transform.position;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, minX, maxX);
        transform.position = clampedPosition;
    }

    private void MoveCameraRight()
    {
        // ���������� �̵�
        transform.Translate(Vector3.right * sensitivity * Time.deltaTime * moveSpeed);

        // ī�޶� �̵� ���� ����
        Vector3 clampedPosition = transform.position;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, minX, maxX);
        transform.position = clampedPosition;
    }

}
