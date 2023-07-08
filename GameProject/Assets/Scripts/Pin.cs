using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Pin : MonoBehaviour
{
    public float uprightThreshold=1f;
    public AudioSource audioSouce;

    private void Awake()
    {
        audioSouce = GetComponent<AudioSource>();
    }
    public bool CheckUpright()
    {
        float angleDifference = Vector3.Angle(transform.up, Vector3.up);
        return angleDifference <= uprightThreshold;
    }
}