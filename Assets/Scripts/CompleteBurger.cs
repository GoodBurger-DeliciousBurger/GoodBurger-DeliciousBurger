using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CompleteBurger : MonoBehaviour
{
    [SerializeField]
    private Transform materialPlace2;
    [SerializeField]
    private List<GameObject> trashPlaces; // trashPlace 리스트로 변경

    public static List<string> reachedObjects = new List<string>();
    public static GameObject completedBread;

    private bool isDragging;
    private Vector2 initialPosition;
    private Vector2 mousePosition;
    private float mouseX, mouseY;
    private Dictionary<GameObject, Vector2> trashInitialPositions; // trashPlace 초기 위치 저장용

    private void Awake()
    {
        isDragging = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        ClearReachedObjects(); // Start 메서드에서 리스트를 초기화합니다.
        initialPosition = transform.position;
        trashInitialPositions = new Dictionary<GameObject, Vector2>();

        // trashPlaces의 초기 위치 저장
        foreach (var trashPlace in trashPlaces)
        {
            trashInitialPositions[trashPlace] = trashPlace.transform.position;
        }
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

            // trashPlaces[1]의 중심 계산
            Vector2 trashCenter = trashPlaces[1].transform.position;
            if (Mathf.Abs(transform.position.x - trashCenter.x) <= 250.0f && Mathf.Abs(transform.position.y - trashCenter.y) <= 250.0f)
            {
                Vector2 targetPosition = trashInitialPositions[trashPlaces[1]] + Vector2.up * 40;
                trashPlaces[1].transform.position = Vector2.Lerp(trashPlaces[1].transform.position, targetPosition, 0.1f);
            }
            else
            {
                // trashPlaces[1]가 원래 위치로 돌아오도록 설정
                trashPlaces[1].transform.position = Vector2.Lerp(trashPlaces[1].transform.position, trashInitialPositions[trashPlaces[1]], 0.1f);
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
            StartCoroutine(LoadGameSceneAfterDelay(1.5f));
        }
        else
        {
            transform.position = initialPosition;
        }

        // trashPlaces[1]를 원래 위치로 되돌림
        trashPlaces[1].transform.position = trashInitialPositions[trashPlaces[1]];
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

    public IEnumerator LoadGameSceneAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("GameScene");
    }
}
