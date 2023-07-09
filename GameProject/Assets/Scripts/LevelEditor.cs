using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class LevelEditor : MonoBehaviour {

    [Header("PLACE LEVEL PLACEABLES HERE")]
    public List<Placeable> levelPlaceables;

    [Range(0.01f, 100f)] public float rotationSpeed = 40f;
    public Material invalidMat;

    [Header("AUDIO")]
    public AudioClip OnPlace;
    public AudioClip OnPickUp;
    public AudioClip OnSelect;
    public AudioClip OnInvalidClick;

    [Header("Info")]
    public List<Placeable> hotbarredPlaceables = new List<Placeable>();
    public Placeable activePlaceable;
    public List<Placeable> placedPlaceables = new List<Placeable>();

    private int floorLayer = 1 << 3;
    private BallMovement ball;
    public AudioSource AudioSource { get; private set; }

    private void Awake()
    {
        AudioSource = GetComponent<AudioSource>();
        ball = FindObjectOfType<BallMovement>();
    }

    void Start()
    {

        foreach(Placeable levelPlaceable in levelPlaceables)
        {
            Placeable p = Instantiate(levelPlaceable);
            hotbarredPlaceables.Add(p);
            p.gameObject.SetActive(false);
        }


    }

    public bool AreAllPinsPlaced() => hotbarredPlaceables.Count == 0 || !hotbarredPlaceables.Any(p => p.TryGetComponent<Pin>(out _));


    //todo only on play mode
    public void AllToInactive()
    {
        while(placedPlaceables.Count > 0)
        {
            Placeable p = placedPlaceables.First();
            XToActive(p);
            ActiveToInactive();
        }
    }

    public void ActiveToInactive()
    {
        activePlaceable.gameObject.SetActive(false);
        activePlaceable = null;

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
            AudioSource.PlayOneShot(OnPickUp, 0.2f);
            hotbarredPlaceables.Add(placeable);
            placeable.transform.rotation = placeable.InitRot;
        }

        placeable.inEditor = true;
        activePlaceable = placeable;

    }

    public void ActiveToFixed()
    {
        AudioSource.PlayOneShot(OnPlace, 0.75f);
        placedPlaceables.Add(activePlaceable);
        hotbarredPlaceables.Remove(activePlaceable);
        activePlaceable.inEditor = false;
        activePlaceable = null;
    }

    void MoveActiveToMousePos()
    {
        Ray castPoint = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(castPoint, out hit, Mathf.Infinity, floorLayer)) //placeable layer
        {

            Vector3 placePos = hit.point + Vector3.up * activePlaceable.heightOffset; //todo adjust placepos height
            activePlaceable.transform.position = placePos;
        } else
        {
            activePlaceable.transform.position = Vector3.down * 999f; //to narnia
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (activePlaceable != null)
        {
            MoveActiveToMousePos();
            int rotationDir = Input.GetKey(KeyCode.Q) ? -1 : 0;
            rotationDir += Input.GetKey(KeyCode.E) ? 1 : 0;
            if (rotationDir != 0) {

                activePlaceable.transform.Rotate(rotationDir * Vector3.up * rotationSpeed * Time.deltaTime, Space.World);
            }

        }

        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject() && !ball.launched)
        {
            if (activePlaceable == null)
            {
                Ray castPoint = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(castPoint, out hit, Mathf.Infinity))
                {
                    if(hit.collider.TryGetComponent<Placeable>(out Placeable placeable))
                    {
                        //Debug.Log("RAYCASTED");
                        XToActive(placeable);
                    }
                    
                }
            }
            else if (activePlaceable.CanPlace)
            {
                Ray castPoint = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(castPoint, out _, Mathf.Infinity, floorLayer))
                {
                    ActiveToFixed();
                }
                

            } else
            {
                AudioSource.PlayOneShot(OnInvalidClick, 0.3f);
            }
        }
    }


}
