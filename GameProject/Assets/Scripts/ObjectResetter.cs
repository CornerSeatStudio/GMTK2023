using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectResetter : MonoBehaviour
{
    [Header("DRAG THINGS YOU WANT TO RESET HERE")]
    public StartMove ball;
    public List<Pin> nonPlaceablePins;
    
    private LevelEditor levelEditor;
    private TransformData initBallTransform;
    private Dictionary<GameObject, TransformData> resetTransformData = new();

    struct TransformData
    {
        public Vector3 position;
        public Quaternion rotation;

        public TransformData(Vector3 position, Quaternion rotation)
        {
            this.position = position;
            this.rotation = rotation;
        }
    }

    private void Awake()
    {
        levelEditor = FindObjectOfType<LevelEditor>();
    }


    public void OnPlay()
    {
        resetTransformData.Clear();

        initBallTransform.position = ball.transform.position;
        initBallTransform.rotation = ball.transform.rotation;

        foreach (Placeable p in levelEditor.placedPlaceables)
        {
            resetTransformData.Add(p.gameObject, new TransformData(p.transform.position, p.transform.rotation));
        }

        foreach (Pin p in nonPlaceablePins)
        {
            resetTransformData.Add(p.gameObject, new TransformData(p.transform.position, p.transform.rotation));
        }

        ball.OnSendLeBall();
    }

    public void OnReset()
    {
        var rb = ball.GetComponent<Rigidbody>();
        rb.isKinematic = true;
    

        ball.transform.position = initBallTransform.position;
        ball.transform.rotation = initBallTransform.rotation;

        foreach(var (go, t) in resetTransformData)
        {
            go.transform.position = t.position;
            go.transform.rotation = t.rotation;
        }

        ball.EditModeUI.SetActive(true);
        ball.PlayModeUI.SetActive(false);
    }
}
