using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drag : MonoBehaviour
{
    [SerializeField]
    private Transform materialPlace;

    private Vector2 initialPosition;
    private Vector2 mousePosition;
    private float mouseX, mouseY;

    private static Dictionary<GameObject, bool> lockedObjects = new Dictionary<GameObject, bool>();
    private static Dictionary<GameObject, Renderer> renderers = new Dictionary<GameObject, Renderer>();

    private void Awake()
    {
        // 게임이 시작될 때마다 모든 오브젝트의 잠금 상태를 초기화함
        ResetLockedStatus();

        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null && !renderers.ContainsKey(gameObject))
        {
            renderers.Add(gameObject, renderer);
        }
    }

    private void Start()
    {
        initialPosition = transform.position;
    }

    private void ResetLockedStatus()
    {
        // 모든 오브젝트의 잠금 상태를 초기화함
        lockedObjects.Clear();
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("material"))
        {
            lockedObjects.Add(obj, false);
        }
    }

    private void OnMouseDown()
    {
        if (!lockedObjects[gameObject])
        {
            // 오브젝트 복사
            GameObject duplicate = Instantiate(gameObject, transform.position, transform.rotation);
            duplicate.GetComponent<Drag>().enabled = true; // 복사된 오브젝트도 드래그 가능하도록 함

            Renderer renderer = duplicate.GetComponent<Renderer>();
            if (renderer != null && !renderers.ContainsKey(duplicate))
            {
                renderers.Add(duplicate, renderer);
            }

            // 새로운 오브젝트의 sortingOrder 설정
            int maxSortingOrder = GetMaxSortingOrder();
            if (renderer != null)
            {
                renderer.sortingOrder = maxSortingOrder + 1;
            }

            // 새로운 오브젝트의 포지션 업데이트
            mouseX = Camera.main.ScreenToWorldPoint(Input.mousePosition).x - duplicate.transform.position.x;
            mouseY = Camera.main.ScreenToWorldPoint(Input.mousePosition).y - duplicate.transform.position.y;

            // 복사된 오브젝트를 드래그 상태로 설정
            duplicate.GetComponent<Drag>().StartDragging();
        }
    }

    public void StartDragging()
    {
        mouseX = Camera.main.ScreenToWorldPoint(Input.mousePosition).x - transform.position.x;
        mouseY = Camera.main.ScreenToWorldPoint(Input.mousePosition).y - transform.position.y;

        // 드래그 중에 sortingOrder를 가장 높은 값으로 설정
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            int maxSortingOrder = GetMaxSortingOrder();
            renderer.sortingOrder = maxSortingOrder + 1;
        }
    }

    private void OnMouseDrag()
    {
        if (!lockedObjects[gameObject])
        {
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector2(mousePosition.x - mouseX, mousePosition.y - mouseY);

            // 드래그 중에 sortingOrder를 계속 가장 높은 값으로 유지
            Renderer renderer = GetComponent<Renderer>();
            if (renderer != null)
            {
                int maxSortingOrder = GetMaxSortingOrder();
                renderer.sortingOrder = maxSortingOrder + 1;
            }
        }
    }

    private void OnMouseUp()
    {
        if (Mathf.Abs(transform.position.x - materialPlace.position.x) <= 90.0f && Mathf.Abs(transform.position.y - materialPlace.position.y) <= 90.0f)
        {
            transform.position = new Vector2(materialPlace.position.x - 50.0f, materialPlace.position.y);
            lockedObjects[gameObject] = true;

            // 잠긴 오브젝트의 sortingOrder 설정
            SetLockedSortingOrder();

            // 오브젝트의 콜라이더 비활성화로 상호작용 차단
            Collider2D collider = GetComponent<Collider2D>();
            if (collider != null)
            {
                collider.enabled = false;
            }
        }
        else
        {
            transform.position = new Vector2(initialPosition.x, initialPosition.y);
        }
    }

    private int GetMaxSortingOrder()
    {
        int maxSortingOrder = 0;
        foreach (KeyValuePair<GameObject, Renderer> pair in renderers)
        {
            if (pair.Value != null)
            {
                maxSortingOrder = Mathf.Max(maxSortingOrder, pair.Value.sortingOrder);
            }
        }
        return maxSortingOrder;
    }

    private void SetLockedSortingOrder()
    {
        // 잠긴 오브젝트의 sortingOrder 중 가장 높은 값을 찾기
        int maxSortingOrder = GetMaxSortingOrder();

        // 현재 오브젝트의 sortingOrder 설정
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.sortingOrder = maxSortingOrder + 1;
        }
    }
}
