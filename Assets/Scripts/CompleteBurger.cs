using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompleteBurger : MonoBehaviour
{

    public static List<string> reachedObjects = new List<string>();

    // Start is called before the first frame update
    void Start()
    {
        ClearReachedObjects(); // Start 메서드에서 리스트를 초기화합니다.
    }

    public static void ClearReachedObjects()
    {
        reachedObjects.Clear();
    }


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
}