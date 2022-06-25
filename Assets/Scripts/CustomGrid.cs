using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomGrid : MonoBehaviour
{
    public float gridSize;
    RaycastHit hit;
    Vector3 movePoint;
    public GameObject prefab;
    bool placeble = true;
    private void Start()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray,out hit))
        {
            movePoint.x = Mathf.Floor(hit.point.x / gridSize) * gridSize;
            movePoint.y = 0.5f;
            movePoint.z = Mathf.Floor(hit.point.z / gridSize) * gridSize;

            transform.position = movePoint;
        }
    }
    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            movePoint.x = Mathf.Floor(hit.point.x / gridSize) * gridSize;
            movePoint.y = 0.5f;
            movePoint.z = Mathf.Floor(hit.point.z / gridSize) * gridSize;

            transform.position = movePoint;
        }
        if (Input.GetMouseButton(1) && placeble)
        {
            Instantiate(prefab, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        placeble = false;
    }
    private void OnTriggerExit(Collider other)
    {
        placeble = true;
    }
}
