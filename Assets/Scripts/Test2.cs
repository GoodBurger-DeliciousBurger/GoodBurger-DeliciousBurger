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
        ClearReachedObjects(); // Start �޼��忡�� ����Ʈ�� �ʱ�ȭ�մϴ�.
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
            StartCoroutine(LoadGameSceneAfterDelay(1.5f)); // 1.5�� �Ŀ� �� ��ȯ
        }
        else
        {
            transform.position = initialPosition;
        }
    }

    // �ϼ��� �ܹ��ŷ� �̹��� �ٲٴ� �Լ�
    public static GameObject DetermineBreadType(List<GameObject> completedBreadPrefabs)
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

    // n���� GameScene���� �̵�
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
        ClearReachedObjects(); // Start �޼��忡�� ����Ʈ�� �ʱ�ȭ�մϴ�.
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

            // �ܹ��� ������ ����
            GameObject completedBreadPrefab = DetermineBreadType(completedBreadPrefabs);
            if (completedBreadPrefab != null)
            {
                Instantiate(completedBreadPrefab, materialPlace2.position, Quaternion.identity);
                StartCoroutine(LoadGameSceneAfterDelay(1.5f)); // 1.5�� �Ŀ� �� ��ȯ
            }
        }
        else
        {
            transform.position = initialPosition;
        }
    }

    // �ϼ��� �ܹ��ŷ� �̹��� �ٲٴ� �Լ�
    public static GameObject DetermineBreadType(List<GameObject> completedBreadPrefabs)
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

    // n�� �� GameScene���� �̵�
    public IEnumerator LoadGameSceneAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("GameScene");
    }
}*/
