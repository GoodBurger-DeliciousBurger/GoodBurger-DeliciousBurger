using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Test2 : MonoBehaviour
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
            mouseX = Input.mousePosition.x - transform.position.x;
            mouseY = Input.mousePosition.y - transform.position.y;
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
            StartCoroutine(LoadGameSceneAfterDelay(1.5f)); // 1.5초 후에 씬 전환
        }
        else
        {
            transform.position = initialPosition;
        }
    }

    // 완성된 햄버거로 이미지 바꾸는 함수
    public static GameObject DetermineBreadType(List<GameObject> completedBreadPrefabs)
    {
        // bottom_bread, lettuce, top_bread 순서로 도달한 경우
        if (reachedObjects.Count == 3 && reachedObjects[0] == "under bread_0" && reachedObjects[1] == "Lettuce_0" && reachedObjects[2] == "top bread_0")
        {
            // 예를 들어 bottom_bread, lettuce, top_bread 순서로 도달하면 CheeseBurger 프리팹 반환
            return completedBreadPrefabs[0]; // 여기에는 CheeseBurger 프리팹을 넣어주세요.
        }
        // 그 외의 경우
        else
        {
            // 다른 조합일 때는 다른 종류의 bread를 반환할 수 있습니다.
            // 여기에는 다른 조합에 해당하는 프리팹을 넣어주세요.
            return null; // 예시로 null을 반환하였습니다. 적절한 프리팹을 반환하도록 수정해주세요.
        }
    }

    // n초후 GameScene으로 이동
    public IEnumerator LoadGameSceneAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("GameScene");
    }

}


/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Test2 : MonoBehaviour
{
    [SerializeField]
    private Transform materialPlace2;

    [SerializeField]
    private List<GameObject> completedBreadPrefabs;

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
            mouseX = Input.mousePosition.x - transform.position.x;
            mouseY = Input.mousePosition.y - transform.position.y;
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

            // 햄버거 프리팹 생성
            GameObject completedBreadPrefab = DetermineBreadType(completedBreadPrefabs);
            if (completedBreadPrefab != null)
            {
                Instantiate(completedBreadPrefab, materialPlace2.position, Quaternion.identity);
                StartCoroutine(LoadGameSceneAfterDelay(1.5f)); // 1.5초 후에 씬 전환
            }
        }
        else
        {
            transform.position = initialPosition;
        }
    }

    // 완성된 햄버거로 이미지 바꾸는 함수
    public static GameObject DetermineBreadType(List<GameObject> completedBreadPrefabs)
    {
        // bottom_bread, lettuce, top_bread 순서로 도달한 경우
        if (reachedObjects.Count == 3 && reachedObjects[0] == "under bread_0" && reachedObjects[1] == "Lettuce_0" && reachedObjects[2] == "top bread_0")
        {
            // 예를 들어 bottom_bread, lettuce, top_bread 순서로 도달하면 CheeseBurger 프리팹 반환
            return completedBreadPrefabs[0]; // 여기에는 CheeseBurger 프리팹을 넣어주세요.
        }
        // 그 외의 경우
        else
        {
            // 다른 조합일 때는 다른 종류의 bread를 반환할 수 있습니다.
            // 여기에는 다른 조합에 해당하는 프리팹을 넣어주세요.
            return null; // 예시로 null을 반환하였습니다. 적절한 프리팹을 반환하도록 수정해주세요.
        }
    }

    // n초 후 GameScene으로 이동
    public IEnumerator LoadGameSceneAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("GameScene");
    }
}*/
