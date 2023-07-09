using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Collider))]
public class Placeable : MonoBehaviour
{

    private Material invalidMat;
    private Collider col;
    private Rigidbody rb;
    private MeshRenderer mr;
    private Material ogMat;

    [Header("Props")]
    public Sprite spriteIcon;
    [Range(-10f, 10f)] public float heightOffset = 0f;

    [Header("Info")]
    public bool inEditor = true;

    [field: SerializeField] public bool CanPlace { get; private set;  } = true;

    private void Awake()
    {
        col = GetComponent<Collider>();
        rb = GetComponent<Rigidbody>();
        mr = GetComponent<MeshRenderer>();
        ogMat = mr.material;
        invalidMat = FindObjectOfType<LevelEditor>().invalidMat;
    }

    private void Update()
    {
        col.isTrigger = inEditor;
        if(rb != null)
        {
            rb.isKinematic = inEditor;
        }
        if (!inEditor)
        {
            mr.material = ogMat;
        }
    }


    //todo invalid placement area neads kinematic rb
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<InvalidPlacementArea>(out InvalidPlacementArea invalidArea))
        {
            if (invalidArea.NoPinsOnly)
            {
                if(TryGetComponent<Pin>(out _))
                {
                    CanPlace = false;
                    if (invalidMat != null)
                        mr.material = invalidMat;
                }
                
            } else
            {
                CanPlace = false;
                if (invalidMat != null)
                    mr.material = invalidMat;
            }

           
            //todo change color?
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<InvalidPlacementArea>(out _))
        {
            CanPlace = true;
            mr.material = ogMat;


        }
    }

}
