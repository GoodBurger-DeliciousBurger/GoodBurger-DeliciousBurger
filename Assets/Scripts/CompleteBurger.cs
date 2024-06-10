using System.Collections.Generic;
using UnityEngine;

public class CompleteBurger : MonoBehaviour
{
    [SerializeField]
    private Transform materialPlace2;

    public static List<string> reachedObjects = new List<string>();
    public static GameObject completedBread;


    private bool isDragging;
    private Vector2 initialPosition;
    private Vector2 mousePosition;
    private float mouseX, mouseY;

    private void Awake()
    {
        isDragging = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        ClearReachedObjects(); // Start 메서드에서 리스트를 초기화합니다.
        initialPosition = transform.position;
    }


    public static void ClearReachedObjects()
    {
        reachedObjects.Clear();
    }

    private void OnMouseDown()
    {
        if (isDragging)
        {
            mouseX = Camera.main.ScreenToWorldPoint(Input.mousePosition).x - transform.position.x;
            mouseY = Camera.main.ScreenToWorldPoint(Input.mousePosition).y - transform.position.y;
        }
    }

    private void OnMouseDrag()
    {
        if (isDragging)
        {
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector2(mousePosition.x - mouseX, mousePosition.y - mouseY);

            Renderer renderer = GetComponent<Renderer>();
            if (renderer != null)
            {
                int maxSortingOrder = GetMaxSortingOrder();
                renderer.sortingOrder = maxSortingOrder + 5;
            }
        }
    }
    private int GetMaxSortingOrder()
    {
        int maxSortingOrder = 0;
        foreach (KeyValuePair<GameObject, Renderer> pair in Drag.renderers)
        {
            if (pair.Value != null)
            {
                maxSortingOrder = Mathf.Max(maxSortingOrder, pair.Value.sortingOrder);
            }
        }
        return maxSortingOrder;
    }

    private void OnMouseUp()
    {
        if (!isDragging) return;
        if (Mathf.Abs(transform.position.x - materialPlace2.position.x) <= 100.0f && Mathf.Abs(transform.position.y - materialPlace2.position.y) <= 100.0f)
        {
            transform.position = new Vector2(materialPlace2.position.x, materialPlace2.position.y);
            isDragging = false;
        }
        else
        {
            transform.position = initialPosition;
        }
    }


    public static GameObject DetermineBreadType(List<GameObject> completedBreadPrefabs)
    {
        // bottom_bread, lettuce, top_bread 순서로 도달한 경우
        if (reachedObjects.Count == 6
            && reachedObjects[0] == "under bread"
            && reachedObjects[1] == "tomato"
            && reachedObjects[2] == "patty"
            && reachedObjects[3] == "teriyaki"
            && reachedObjects[4] == "lettuce"
            && reachedObjects[5] == "top bread")
        {
            return completedBreadPrefabs[0]; 
        }
        else if (reachedObjects.Count == 6
            && reachedObjects[0] == "under bread"
            && reachedObjects[1] == "patty"
            && reachedObjects[2] == "cheese"
            && reachedObjects[3] == "teriyaki"
            && reachedObjects[4] == "lettuce"
            && reachedObjects[5] == "top bread")
        {
            return completedBreadPrefabs[1];
        }
        else if (reachedObjects.Count == 8
            && reachedObjects[0] == "under bread"
            && reachedObjects[1] == "tomato"
            && reachedObjects[2] == "patty"
            && reachedObjects[3] == "lettuce"
            && reachedObjects[4] == "patty"
            && reachedObjects[5] == "teriyaki"
            && reachedObjects[6] == "lettuce"
            && reachedObjects[7] == "top bread")
        {
            return completedBreadPrefabs[2];
        }
        else if (reachedObjects.Count == 6
            && reachedObjects[0] == "under bread"
            && reachedObjects[1] == "tomato"
            && reachedObjects[2] == "chiken"
            && reachedObjects[3] == "hot"
            && reachedObjects[4] == "lettuce"
            && reachedObjects[5] == "top bread")
        {
            return completedBreadPrefabs[3];
        }
        else if (reachedObjects.Count == 6
            && reachedObjects[0] == "under bread"
            && reachedObjects[1] == "tomato"
            && reachedObjects[2] == "shrimp"
            && reachedObjects[3] == "tartar"
            && reachedObjects[4] == "lettuce"
            && reachedObjects[5] == "top bread")
        {
            return completedBreadPrefabs[4];
        }
        else
        {
            return completedBreadPrefabs[5];
        }
    }

}