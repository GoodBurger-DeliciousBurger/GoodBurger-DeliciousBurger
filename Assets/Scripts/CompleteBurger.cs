using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompleteBurger : MonoBehaviour
{

    public static List<string> reachedObjects = new List<string>();

    // Start is called before the first frame update
    void Start()
    {
        ClearReachedObjects(); // Start �޼��忡�� ����Ʈ�� �ʱ�ȭ�մϴ�.
    }

    public static void ClearReachedObjects()
    {
        reachedObjects.Clear();
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