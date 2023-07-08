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

    [Header("Rotation Sensitivities")]
    [Range(0.01f, 100f)] public float rotationSpeed = 10f;

    [Header("Info")]
    public List<Placeable> hotbarredPlaceables = new List<Placeable>();
    public Placeable activePlaceable;
    public List<Placeable> placedPlaceables = new List<Placeable>();

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

        if (placedPlaceables.Remove(placeable))
        {
            hotbarredPlaceables.Add(placeable);
        }

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
            int rotationDir = Input.GetKey(KeyCode.Q) ? 1 : 0;
            rotationDir += Input.GetKey(KeyCode.E) ? -1 : 0;
            if (rotationDir != 0) {

                activePlaceable.transform.Rotate(rotationDir * Vector3.up * rotationSpeed * Time.deltaTime, Space.World);
            }

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
