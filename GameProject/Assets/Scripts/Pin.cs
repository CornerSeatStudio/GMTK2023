using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pin : MonoBehaviour
{
    public float uprightThreshold=1f;

    public bool CheckUpright()
    {
        float angleDifference = Vector3.Angle(transform.up, Vector3.up);
        return angleDifference <= uprightThreshold;
    }
}