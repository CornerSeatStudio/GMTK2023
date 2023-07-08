using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEditor.PackageManager.Requests;
using UnityEngine;
using UnityEngine.EventSystems;

public class LevelEditor : MonoBehaviour {

    [Header("PLACE LEVEL PLACEABLES HERE")]
    public List<Placeable> levelPlaceables;

    [Header("Info")]
    public List<Placeable> hotbarredPlaceables; 
    public Placeable activePlaceable;
    public List<Placeable> placedPlaceables;

    private int ignoreFloor = 1 << 3;


    void Start()
    {
        foreach(Placeable levelPlaceable in levelPlaceables)
        {
            Placeable p = Instantiate(levelPlaceable);
            hotbarredPlaceables.Add(p);
            p.gameObject.SetActive(false);
        }

    }

    public void ActiveToInactive()
    {
        activePlaceable.gameObject.SetActive(false);
    }

    public void XToActive(Placeable placeable)
    {
        if(activePlaceable != null)
        {
            ActiveToInactive();
        }

        placeable.gameObject.SetActive(true);
        
        placedPlaceables.Remove(placeable); //note this won't always succeed
        hotbarredPlaceables.Add(placeable);

        placeable.inEditor = true;
        activePlaceable = placeable;

    }

    public void ActiveToFixed()
    {
        placedPlaceables.Add(activePlaceable);
        hotbarredPlaceables.Remove(activePlaceable);
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

        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
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
