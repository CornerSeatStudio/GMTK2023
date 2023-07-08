using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEditor.PackageManager.Requests;
using UnityEngine;
using UnityEngine.EventSystems;

public class LevelEditor : MonoBehaviour {

    [Header("Info")]
    public List<Placeable> inactivePlaceables; 
    public Placeable activePlaceable;
    public List<Placeable> placedPlaceables;

    private int ignoreFloor = 1 << 3;

    void Start()
    {
        
        
    }

    void ActiveToInactive(Placeable placeable)
    {
        inactivePlaceables.Add(placeable);
        placeable.gameObject.SetActive(false);
    }

    void XToActive(Placeable placeable)
    {
        placeable.gameObject.SetActive(true);
        //note only one list will have it
        inactivePlaceables.Remove(placeable);
        placedPlaceables.Remove(placeable);


        placeable.inEditor = true;
        activePlaceable = placeable;

    }

    void ActiveToFixed()
    {
        placedPlaceables.Add(activePlaceable);
        activePlaceable.inEditor = false;
        activePlaceable = null;
    }

    void MoveActiveToMousePos()
    {
        Ray castPoint = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(castPoint, out hit, Mathf.Infinity, ignoreFloor))
        {

            Vector3 placePos = hit.point; //todo adjust placepos height
            activePlaceable.transform.position = hit.point;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (activePlaceable != null)
        {
            MoveActiveToMousePos();
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (activePlaceable == null)
            {
                Ray castPoint = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(castPoint, out hit, Mathf.Infinity) && hit.collider.TryGetComponent<Placeable>(out Placeable placeable))
                {
                    XToActive(placeable);
                }
            }
            else if (activePlaceable.CanPlace)
            {
                ActiveToFixed();

            }
        }
    }


}
