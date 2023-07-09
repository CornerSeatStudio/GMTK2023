using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Collider))]
public class SpeedModifyZone : MonoBehaviour
{
    private Collider m_Collider;


    private void Awake()
    {
        m_Collider = GetComponent<Collider>();
        if (!m_Collider.isTrigger)
        {
            Debug.LogError("Speed Modify Zone must be a trigger collider");
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        //if(other.TryGetComponent<>)
    }


    protected void DoTheMagic()
    {

    }
}
