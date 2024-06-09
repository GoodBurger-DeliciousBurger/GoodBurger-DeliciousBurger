using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompleteBurger : MonoBehaviour
{
    [SerializeField]
    private Transform materialPlace2;

    public static List<string> reachedObjects = new List<string>();
    public static GameObject completedBread;

    private bool isDragging = false;
    private Vector2 initialPosition;
    private Vector2 mousePosition;
    private float mouseX, mouseY;

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
        isDragging = true;
        mouseX = Input.mousePosition.x - transform.position.x;
        mouseY = Input.mousePosition.y - transform.position.y;
    }

    private void OnMouseDrag()
    {
        if (isDragging)
        {
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector2(mousePosition.x - mouseX, mousePosition.y - mouseY);
        }
    }

    private void OnMouseUp()
    {
        if (isDragging) return;
        if (Mathf.Abs(transform.position.x - materialPlace2.position.x) <= 90.0f && Mathf.Abs(transform.position.y - materialPlace2.position.y) <= 90.0f)
        {
            transform.position = new Vector2(materialPlace2.position.x, materialPlace2.position.y);
            isDragging = false;
        }
    }


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

}