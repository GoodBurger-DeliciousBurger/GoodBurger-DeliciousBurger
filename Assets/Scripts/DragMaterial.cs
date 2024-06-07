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
        // ������ ���۵� ������ ��� ������Ʈ�� ��� ���¸� �ʱ�ȭ��
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
        // ��� ������Ʈ�� ��� ���¸� �ʱ�ȭ��
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
            // ������Ʈ ����
            GameObject duplicate = Instantiate(gameObject, transform.position, transform.rotation);
            duplicate.GetComponent<Drag>().enabled = true; // ����� ������Ʈ�� �巡�� �����ϵ��� ��

            Renderer renderer = duplicate.GetComponent<Renderer>();
            if (renderer != null && !renderers.ContainsKey(duplicate))
            {
                renderers.Add(duplicate, renderer);
            }

            // ���ο� ������Ʈ�� sortingOrder ����
            int maxSortingOrder = GetMaxSortingOrder();
            if (renderer != null)
            {
                renderer.sortingOrder = maxSortingOrder + 1;
            }

            // ���ο� ������Ʈ�� ������ ������Ʈ
            mouseX = Camera.main.ScreenToWorldPoint(Input.mousePosition).x - duplicate.transform.position.x;
            mouseY = Camera.main.ScreenToWorldPoint(Input.mousePosition).y - duplicate.transform.position.y;

            // ����� ������Ʈ�� �巡�� ���·� ����
            duplicate.GetComponent<Drag>().StartDragging();
        }
    }

    public void StartDragging()
    {
        mouseX = Camera.main.ScreenToWorldPoint(Input.mousePosition).x - transform.position.x;
        mouseY = Camera.main.ScreenToWorldPoint(Input.mousePosition).y - transform.position.y;

        // �巡�� �߿� sortingOrder�� ���� ���� ������ ����
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

            // �巡�� �߿� sortingOrder�� ��� ���� ���� ������ ����
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

            // ��� ������Ʈ�� sortingOrder ����
            SetLockedSortingOrder();

            // ������Ʈ�� �ݶ��̴� ��Ȱ��ȭ�� ��ȣ�ۿ� ����
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
        // ��� ������Ʈ�� sortingOrder �� ���� ���� ���� ã��
        int maxSortingOrder = GetMaxSortingOrder();

        // ���� ������Ʈ�� sortingOrder ����
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.sortingOrder = maxSortingOrder + 1;
        }
    }
}
