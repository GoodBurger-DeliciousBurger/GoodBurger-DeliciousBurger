using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // SceneManager�� ����ϱ� ���� �ʿ�

public class Drag : MonoBehaviour
{
    [SerializeField]
    private Transform materialPlace;
    [SerializeField]
    private List<GameObject> completedBreadPrefabs; // �ϼ��� bread ������ ����Ʈ

    private Vector2 initialPosition;
    private Vector2 mousePosition;
    private float mouseX, mouseY;

    private static Dictionary<GameObject, Renderer> renderers = new Dictionary<GameObject, Renderer>();
    private static bool isCompleted = false; // �ϼ� ���θ� üũ�ϴ� ����
    private static Dictionary<GameObject, bool> lockedObjects = new Dictionary<GameObject, bool>();
    private static List<GameObject> reachedCopies = new List<GameObject>();

    
    private void Awake()
    {
        ResetLockedStatus();
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null && !renderers.ContainsKey(gameObject))
        {
            renderers.Add(gameObject, renderer);
        }

    }


    private void Start()
    {
        isCompleted = false; // isCompleted ���� �ʱ�ȭ
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
        // top bread_0�� ��ǥ ��ġ�� �������� ��� �ٸ� ������Ʈ���� Ŭ������ �ʵ��� ��
        if (isCompleted || (CompleteBurger.reachedObjects.Contains("top bread_0")))
        {
            foreach (GameObject obj in GameObject.FindGameObjectsWithTag("material"))
            {
                lockedObjects[obj] = true;
            }
            return;
        }

        if (!lockedObjects[gameObject] && !isCompleted)
        {
            if (!(CompleteBurger.reachedObjects.Contains("top bread_0")))
            {
                if (gameObject.CompareTag("material"))
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
                }

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
            CompleteBurger.reachedObjects.Add(gameObject.name);
            reachedCopies.Add(gameObject); // ��ǥ ��ġ�� ������ ������Ʈ�� ����Ʈ�� �߰�

            // top_bread_0 ������Ʈ�� ��ǥ ��ġ�� �������� �� ó��
            if (gameObject.name == "top bread_0")
            {
                CompleteBread();
            }
        }
        else
        {
            // ��ǥ ��ġ�� �������� ���� ������Ʈ�� �����
            if (isCompleted || !(CompleteBurger.reachedObjects.Contains("top bread_0")))
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

        foreach (string reachedObject in CompleteBurger.reachedObjects)
        {
            Debug.Log(reachedObject);
        }

        MadeBurger();

        // ������ ������Ʈ���� ����
        CompleteBurger.reachedObjects.Clear();

        // ��� ������Ʈ�� �巡�� ��Ȱ��ȭ
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("material"))
        {
            obj.GetComponent<Drag>().enabled = false;
        }

        // ��ǥ ��ġ�� ������ ���纻 ������Ʈ ����
        foreach (GameObject copy in reachedCopies)
        {
            Destroy(copy);
        }
        reachedCopies.Clear();

    }
    private void MadeBurger()
    {
        transform.position = new Vector2(materialPlace.position.x - 50.0f, materialPlace.position.y);

        // �ϼ��� bread ���� (������ ������Ʈ ����Ʈ�� ������� ����)
        GameObject completedBreadPrefab = CompleteBurger.DetermineBreadType(completedBreadPrefabs);
        if (completedBreadPrefab != null)
        {
            CompleteBurger.completedBread = Instantiate(completedBreadPrefab, transform.position, Quaternion.identity);
        }


    }


}
