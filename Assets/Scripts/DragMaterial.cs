using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drag : MonoBehaviour
{
    [SerializeField]
    private Transform materialPlace;
    [HideInInspector]
    private List<GameObject> completedBreadPrefabs; // �ϼ��� bread ������ ����Ʈ

    private Vector2 initialPosition;
    private Vector2 mousePosition;
    private float mouseX, mouseY;

    private static Dictionary<GameObject, bool> lockedObjects = new Dictionary<GameObject, bool>();
    private static Dictionary<GameObject, Renderer> renderers = new Dictionary<GameObject, Renderer>();
    private static List<string> reachedObjects = new List<string>(); // ������ ������Ʈ ����Ʈ
    private static bool isCompleted = false; // �ϼ� ���θ� üũ�ϴ� ����

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
        reachedObjects.Clear();
    }

    private void OnMouseDown()
    {
        // top_bread_0�� ��ǥ ��ġ�� �������� ��� �ٸ� ������Ʈ���� Ŭ������ �ʵ��� ��
        if (isCompleted || (reachedObjects.Contains("top bread_0")))
        {
            foreach (GameObject obj in GameObject.FindGameObjectsWithTag("material"))
            {
                lockedObjects[obj] = true;
            }
            return;
        }

        if (!lockedObjects[gameObject] && !isCompleted)
        {
            if (!(reachedObjects.Contains("top bread_0")))
            {
                // ������Ʈ ����
                GameObject duplicate = Instantiate(gameObject, transform.position, transform.rotation);
                duplicate.name = gameObject.name;

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

                // ���� ������Ʈ�� ���� ��ġ�� �ǵ�����
                transform.position = initialPosition;
            }

        }
    }

    private void OnMouseDrag()
    {
        if (isCompleted) return;
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
        if (isCompleted) return;
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

            // ������Ʈ�� ��ǥ ��ġ�� ���������� ���
            reachedObjects.Add(gameObject.name);
            // top_bread_0 ������Ʈ�� ��ǥ ��ġ�� �������� �� ó��
            if (gameObject.name == "top_bread_0")
            {
                CompleteBread();
            }
        }
        else
        {
            // ��ǥ ��ġ�� �������� ���� ������Ʈ�� �����
            if (isCompleted || !(reachedObjects.Contains("top bread_0")))
            {
                Destroy(gameObject);
            }
            
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

    private void CompleteBread()
    {
        isCompleted = true;

        // ��ǥ ��ġ�� ������ ������Ʈ�� �׷�ȭ
        List<GameObject> reachedObjectsList = new List<GameObject>();
        foreach (string objName in reachedObjects)
        {
            GameObject obj = GameObject.Find(objName);
            if (obj != null)
            {
                reachedObjectsList.Add(obj);
            }
        }

        // �ϼ��� bread ���� (������ ������Ʈ ����Ʈ�� ������� ����)
        GameObject completedBreadPrefab = DetermineBreadType();
        if (completedBreadPrefab != null)
        {
            Instantiate(completedBreadPrefab, materialPlace.position, Quaternion.identity);
        }

        // ������ ������Ʈ���� ����
        foreach (GameObject obj in reachedObjects)
        {
            obj.SetActive(false);
        }

        // ��� ������Ʈ�� �巡�� ��Ȱ��ȭ
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("material"))
        {
            obj.GetComponent<Drag>().enabled = false;
        }
    }


    private GameObject DetermineBreadType()
    {
        // bottom_bread, lettuce, top_bread ������ ������ ���
        if (reachedObjects.Count == 3 && reachedObjects[0] == "under bread_0" && reachedObjects[1] == "Lettuce_0" && reachedObjects[2] == "top bread_0")
        {
            // ���� ��� bottom_bread, lettuce, top_bread ������ �����ϸ� CheeseBurger ������ ��ȯ
            return completedBreadPrefabs[0]; // ���⿡�� CheeseBurger �������� �־��ּ���.
        }
        // �� ���� ���
        else
        {
            // �ٸ� ������ ���� �ٸ� ������ bread�� ��ȯ�� �� �ֽ��ϴ�.
            // ���⿡�� �ٸ� ���տ� �ش��ϴ� �������� �־��ּ���.
            return null; // ���÷� null�� ��ȯ�Ͽ����ϴ�. ������ �������� ��ȯ�ϵ��� �������ּ���.
        }
    }
}
