using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class isUpright : MonoBehaviour
{
    public float uprightThreshold=1f;

    public bool IsUpright()
    {
        float angleDifference = Vector3.Angle(transform.up, Vector3.up);
        return angleDifference <= uprightThreshold;
    }
}