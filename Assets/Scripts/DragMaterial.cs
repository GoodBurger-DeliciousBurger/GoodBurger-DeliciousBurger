using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // SceneManager를 사용하기 위해 필요

public class Drag : MonoBehaviour
{
    [SerializeField]
    private Transform materialPlace;
    [SerializeField]
    private List<GameObject> completedBreadPrefabs; // 완성된 bread 프리팹 리스트

    private Vector2 initialPosition;
    private Vector2 mousePosition;
    private float mouseX, mouseY;

    private static Dictionary<GameObject, Renderer> renderers = new Dictionary<GameObject, Renderer>();
    private static bool isCompleted = false; // 완성 여부를 체크하는 변수
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
        isCompleted = false; // isCompleted 변수 초기화
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
    }




    private void OnMouseDown()
    {
        // top bread_0가 목표 위치에 도달했을 경우 다른 오브젝트들이 클릭되지 않도록 함
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
            CompleteBurger.reachedObjects.Add(gameObject.name);
            reachedCopies.Add(gameObject); // 목표 위치에 도달한 오브젝트를 리스트에 추가

            // top_bread_0 오브젝트가 목표 위치에 도달했을 때 처리
            if (gameObject.name == "top bread_0")
            {
                CompleteBread();
            }
        }
        else
        {
            // 목표 위치에 도달하지 못한 오브젝트는 사라짐
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

        foreach (string reachedObject in CompleteBurger.reachedObjects)
        {
            Debug.Log(reachedObject);
        }

        MadeBurger();

        // 도달한 오브젝트들을 삭제
        CompleteBurger.reachedObjects.Clear();

        // 모든 오브젝트의 드래그 비활성화
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("material"))
        {
            obj.GetComponent<Drag>().enabled = false;
        }

        // 목표 위치에 도달한 복사본 오브젝트 삭제
        foreach (GameObject copy in reachedCopies)
        {
            Destroy(copy);
        }
        reachedCopies.Clear();

    }
    private void MadeBurger()
    {
        transform.position = new Vector2(materialPlace.position.x - 50.0f, materialPlace.position.y);

        // 완성된 bread 생성 (도달한 오브젝트 리스트를 기반으로 결정)
        GameObject completedBreadPrefab = CompleteBurger.DetermineBreadType(completedBreadPrefabs);
        if (completedBreadPrefab != null)
        {
            CompleteBurger.completedBread = Instantiate(completedBreadPrefab, transform.position, Quaternion.identity);
        }


    }


}
