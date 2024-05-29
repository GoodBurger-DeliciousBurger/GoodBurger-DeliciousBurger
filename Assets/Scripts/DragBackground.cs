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
    public float sensitivity = 10.0f; // 마우스 감도
    public float moveSpeed = 10.0f;   // 카메라 이동 속도
    public float minX = 792.0f;       // 카메라 이동 최소 x 좌표
    public float maxX = 1146.0f;        // 카메라 이동 최대 x 좌표

    private void Update()
    {

        // 마우스 위치가 화면의 좌우 끝에 닿았는지 확인
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
        // 왼쪽으로 이동
        transform.Translate(Vector3.left * sensitivity * Time.deltaTime * moveSpeed);

        // 카메라 이동 범위 제한
        Vector3 clampedPosition = transform.position;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, minX, maxX);
        transform.position = clampedPosition;
    }

    private void MoveCameraRight()
    {
        // 오른쪽으로 이동
        transform.Translate(Vector3.right * sensitivity * Time.deltaTime * moveSpeed);

        // 카메라 이동 범위 제한
        Vector3 clampedPosition = transform.position;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, minX, maxX);
        transform.position = clampedPosition;
    }

}
