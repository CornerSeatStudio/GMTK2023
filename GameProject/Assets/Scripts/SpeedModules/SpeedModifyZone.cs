using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Collider))]
public abstract class SpeedModifyZone : MonoBehaviour
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
        if (other.TryGetComponent<BallMovement>(out BallMovement ball)){
            Debug.Log("SHAGGA");
            DoTheMagic(ball);
        }
    }


    protected abstract void DoTheMagic(BallMovement ball);

}
