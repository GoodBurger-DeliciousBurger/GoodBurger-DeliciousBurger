using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Drag : MonoBehaviour
{
    [SerializeField]
    private Transform materialPlace;

    private Vector2 initialPosition;

    private Vector2 mousePosition;

    private float mouseX, mouseY;

    private static Dictionary<GameObject, bool> lockedObjects = new Dictionary<GameObject, bool>();

    private static Dictionary<GameObject, Renderer> renderers = new Dictionary<GameObject, Renderer>();


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
    }

    private void OnMouseDown()
    {
        if (!lockedObjects[gameObject])
        {
            mouseX = Camera.main.ScreenToWorldPoint(Input.mousePosition).x - transform.position.x;
            mouseY = Camera.main.ScreenToWorldPoint(Input.mousePosition).y - transform.position.y;

            int maxSortingOrder = 0;
            foreach (KeyValuePair<GameObject, Renderer> pair in renderers)
            {
                maxSortingOrder = Mathf.Max(maxSortingOrder, pair.Value.sortingOrder);
            }

            renderers[gameObject].sortingOrder = maxSortingOrder + 1;
        }
    }

    private void OnMouseDrag()
    {
        if (!lockedObjects[gameObject])
        {
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector2(mousePosition.x - mouseX, mousePosition.y - mouseY);
        }
    }

    private void OnMouseUp()
    {
        if(Mathf.Abs(transform.position.x - materialPlace.position.x) <= 90.0f && Mathf.Abs(transform.position.y - materialPlace.position.y) <= 90.0f)
        {
            transform.position = new Vector2(materialPlace.position.x-50.0f, materialPlace.position.y);
            lockedObjects[gameObject]= true;
        }
        else
        {
            transform.position = new Vector2(initialPosition.x, initialPosition.y);
        }
    }

}
