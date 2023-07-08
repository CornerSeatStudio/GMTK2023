using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Placeable : MonoBehaviour
{

    [Header("Dependencies")]
    public Collider col;

    [Header("Info")]
    public bool inEditor = true;

    [field: SerializeField] public bool CanPlace { get; private set;  } = true;

    private void Awake()
    {
        col = GetComponent<Collider>();
    }

    private void Update()
    {
        col.isTrigger = inEditor;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<InvalidPlacementArea>(out _))
        {
            CanPlace = false;
            //todo change color?
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<InvalidPlacementArea>(out _))
        {
            CanPlace = true;
        }
    }

}
