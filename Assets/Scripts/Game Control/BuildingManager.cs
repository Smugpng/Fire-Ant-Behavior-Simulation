using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BuildingManager : MonoBehaviour
{
    public GameObject[] gameObjects;
    private GameObject pendingObj;

    private Vector3 pos;

    private RaycastHit hit;

    [SerializeField] private LayerMask ground;

    // Update is called once per frame
    void Update()
    {
        if(pendingObj != null)
        {
            pendingObj.transform.position = pos;
            if(Input.GetMouseButtonDown(0))
            {
                PlaceObject();
            }
        }
    }
    public void PlaceObject()
    {
        pendingObj = null;
    }
    private void FixedUpdate() //Put all Physics stuff here
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out hit, 1000, ground)) 
        { 
            pos = hit.point;
        }
    }

    public void SelectObject(int index)
    {
        pendingObj = Instantiate(gameObjects[index], pos, transform.rotation);
    }
}
