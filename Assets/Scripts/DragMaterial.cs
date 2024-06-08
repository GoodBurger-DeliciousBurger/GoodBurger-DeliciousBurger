using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drag : MonoBehaviour
{
    [SerializeField]
    private Transform materialPlace;
    [HideInInspector]
    private List<GameObject> completedBreadPrefabs; // 완성된 bread 프리팹 리스트

    private Vector2 initialPosition;
    private Vector2 mousePosition;
    private float mouseX, mouseY;

    private static Dictionary<GameObject, bool> lockedObjects = new Dictionary<GameObject, bool>();
    private static Dictionary<GameObject, Renderer> renderers = new Dictionary<GameObject, Renderer>();
    private static List<string> reachedObjects = new List<string>(); // 도달한 오브젝트 리스트
    private static bool isCompleted = false; // 완성 여부를 체크하는 변수

    private void Awake()
    {
        // 게임이 시작될 때마다 모든 오브젝트의 잠금 상태를 초기화함
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
        // 모든 오브젝트의 잠금 상태를 초기화함
        lockedObjects.Clear();
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("material"))
        {
            lockedObjects.Add(obj, false);
        }
        reachedObjects.Clear();
    }

    private void OnMouseDown()
    {
        // top_bread_0가 목표 위치에 도달했을 경우 다른 오브젝트들이 클릭되지 않도록 함
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
                // 오브젝트 복사
                GameObject duplicate = Instantiate(gameObject, transform.position, transform.rotation);
                duplicate.name = gameObject.name;

                Renderer renderer = duplicate.GetComponent<Renderer>();
                if (renderer != null && !renderers.ContainsKey(duplicate))
                {
                    renderers.Add(duplicate, renderer);
                }

                // 새로운 오브젝트의 sortingOrder 설정
                int maxSortingOrder = GetMaxSortingOrder();
                if (renderer != null)
                {
                    renderer.sortingOrder = maxSortingOrder + 1;
                }

                // 원본 오브젝트를 원래 위치로 되돌리기
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

            // 드래그 중에 sortingOrder를 계속 가장 높은 값으로 유지
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

            // 잠긴 오브젝트의 sortingOrder 설정
            SetLockedSortingOrder();

            // 오브젝트의 콜라이더 비활성화로 상호작용 차단
            Collider2D collider = GetComponent<Collider2D>();
            if (collider != null)
            {
                collider.enabled = false;
            }

            // 오브젝트가 목표 위치에 도달했음을 기록
            reachedObjects.Add(gameObject.name);
            // top_bread_0 오브젝트가 목표 위치에 도달했을 때 처리
            if (gameObject.name == "top_bread_0")
            {
                CompleteBread();
            }
        }
        else
        {
            // 목표 위치에 도달하지 못한 오브젝트는 사라짐
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
        // 잠긴 오브젝트의 sortingOrder 중 가장 높은 값을 찾기
        int maxSortingOrder = GetMaxSortingOrder();

        // 현재 오브젝트의 sortingOrder 설정
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.sortingOrder = maxSortingOrder + 1;
        }
    }

    private void CompleteBread()
    {
        isCompleted = true;

        // 목표 위치에 도달한 오브젝트만 그룹화
        List<GameObject> reachedObjectsList = new List<GameObject>();
        foreach (string objName in reachedObjects)
        {
            GameObject obj = GameObject.Find(objName);
            if (obj != null)
            {
                reachedObjectsList.Add(obj);
            }
        }

        // 완성된 bread 생성 (도달한 오브젝트 리스트를 기반으로 결정)
        GameObject completedBreadPrefab = DetermineBreadType();
        if (completedBreadPrefab != null)
        {
            Instantiate(completedBreadPrefab, materialPlace.position, Quaternion.identity);
        }

        // 도달한 오브젝트들을 삭제
        foreach (GameObject obj in reachedObjects)
        {
            obj.SetActive(false);
        }

        // 모든 오브젝트의 드래그 비활성화
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("material"))
        {
            obj.GetComponent<Drag>().enabled = false;
        }
    }


    private GameObject DetermineBreadType()
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
