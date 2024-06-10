using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.SceneManagement; // SceneManager�� ����ϱ� ���� �ʿ�

public class Drag : MonoBehaviour
{
    [SerializeField]
    private Transform materialPlace;
    [SerializeField]
    private List<GameObject> completedBreadPrefabs; // �ϼ��� bread ������ ����Ʈ
    [SerializeField]
    private List<GameObject> substituteObject; // ��ü�� ������Ʈ ������

    private Vector2 initialPosition;
    private Vector2 mousePosition;
    private float mouseX, mouseY;
    private int initialLayer;

    public static Dictionary<GameObject, Renderer> renderers = new Dictionary<GameObject, Renderer>();
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
        initialLayer = gameObject.layer;
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
        // top bread�� ��ǥ ��ġ�� �������� ��� �ٸ� ������Ʈ���� Ŭ������ �ʵ��� ��
        if (isCompleted || (CompleteBurger.reachedObjects.Contains("top bread")))
        {
            foreach (GameObject obj in GameObject.FindGameObjectsWithTag("material"))
            {
                lockedObjects[obj] = true;
            }
            return;
        }

        if (!lockedObjects[gameObject] && !isCompleted)
        {
            if (!(CompleteBurger.reachedObjects.Contains("top bread")))
            {
                if (gameObject.CompareTag("material"))
                {

                    if (!gameObject.name.Contains("sauce"))
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
    GameObject sauce;
    private void OnMouseUp()
    {
        if (isCompleted) return;
        if (Mathf.Abs(transform.position.x - materialPlace.position.x) <= 90.0f && Mathf.Abs(transform.position.y - materialPlace.position.y) <= 90.0f)
        {

            if (gameObject.name.Contains("sauce"))
            {
                transform.position = initialPosition;

                // TableCloth ������Ʈ�� sortingOrder ���� �����ͼ� �� �Ʒ��� ����
                GameObject tableCloth = GameObject.Find("Tablecloth");
                if (tableCloth != null)
                {
                    Renderer tableClothRenderer = tableCloth.GetComponent<Renderer>();
                    if (tableClothRenderer != null)
                    {
                        Renderer renderer = gameObject.GetComponent<Renderer>();
                        if (renderer != null)
                        {
                            renderer.sortingOrder = tableClothRenderer.sortingOrder - 1;
                        }
                    }
                }
                // Ư�� �̸��� ���� ������Ʈ�� ��ǥ ��ġ�� �����ϸ� ��ü ������Ʈ ����
                if (substituteObject != null)
                {
                    switch (gameObject.name)
                    {
                        case "teriyaki sauce":
                            sauce=Instantiate(substituteObject[0], new Vector2(materialPlace.position.x - 50.0f, materialPlace.position.y), Quaternion.identity);
                            break;
                        case "tartar sauce":
                            sauce=Instantiate(substituteObject[1], new Vector2(materialPlace.position.x - 50.0f, materialPlace.position.y), Quaternion.identity);
                            break;
                        case "hot sauce":
                            sauce = Instantiate(substituteObject[2], new Vector2(materialPlace.position.x - 50.0f, materialPlace.position.y), Quaternion.identity);
                            break;
                    }
                   
                }
                Renderer renderer_sauce = sauce.GetComponent<Renderer>();
                if (renderer_sauce != null && !renderers.ContainsKey(sauce))
                {
                    renderers.Add(sauce, renderer_sauce);
                }

                // ���ο� ������Ʈ�� sortingOrder ����
                int maxSortingOrder = GetMaxSortingOrder();
                if (renderer_sauce != null)
                {
                    renderer_sauce.sortingOrder = maxSortingOrder + 1;
                }

                CompleteBurger.reachedObjects.Add(sauce.name);
                reachedCopies.Add(sauce);

            }
            else
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

                // top_bread ������Ʈ�� ��ǥ ��ġ�� �������� �� ó��
                if (gameObject.name == "top bread")
                {
                    CompleteBread();
                }
            }
        }
        else
        {
            // ��ǥ ��ġ�� �������� ���� ������Ʈ�� �����

            if (gameObject.name.Contains("sauce"))
            {
                // ��ǥ ��ġ�� �������� ���� �ҽ��� ���� ��ġ�� ���ư�
                transform.position = initialPosition;

                // TableCloth ������Ʈ�� sortingOrder ���� �����ͼ� �� �Ʒ��� ����
                GameObject tableCloth = GameObject.Find("Tablecloth");
                if (tableCloth != null)
                {
                    Renderer tableClothRenderer = tableCloth.GetComponent<Renderer>();
                    if (tableClothRenderer != null)
                    {
                        Renderer renderer = gameObject.GetComponent<Renderer>();
                        if (renderer != null)
                        {
                            renderer.sortingOrder = tableClothRenderer.sortingOrder - 1;
                        }
                    }
                }
            }
            else if (isCompleted || !(CompleteBurger.reachedObjects.Contains("top bread")))
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
