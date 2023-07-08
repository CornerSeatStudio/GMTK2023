using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Pin : MonoBehaviour
{
    public AudioClip fallenSoundEffect;

    public float uprightThreshold=1f; //todo many need to increase
    public AudioSource audioSouce;

    
    private void Awake()
    {
        audioSouce = GetComponent<AudioSource>();
        audioSouce.clip = fallenSoundEffect;
    }
    public bool IsUpright()
    {
        float angleDifference = Vector3.Angle(transform.up, Vector3.up);
        return angleDifference <= uprightThreshold;
    }
}