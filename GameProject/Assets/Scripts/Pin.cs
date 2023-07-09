using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Pin : MonoBehaviour
{
    public AudioClip fallenSoundEffect;

    public float uprightThreshold=10f; //todo many need to increase
    public AudioSource audioSouce;

    
    private void Awake()
    {
        audioSouce = GetComponent<AudioSource>();
        audioSouce.clip = fallenSoundEffect;
    }

    private void Start()
    {
        
    }
    public bool IsUpright()
    {
        float angleDifference = Vector3.Angle(transform.up, Vector3.up);
        return angleDifference <= uprightThreshold;
    }

    private void Update()
    {
        cooldown -= Time.deltaTime;
    }


    float cooldown = 0f;
    private void OnCollisionEnter(Collision collision)
    {
        if (IsUpright() && cooldown <= 0f && collision.gameObject.TryGetComponent<BallMovement>(out _) || collision.gameObject.TryGetComponent<Pin>(out _))
        {
            audioSouce.pitch = UnityEngine.Random.Range(0.8f, 1.4f);
            audioSouce.PlayOneShot(fallenSoundEffect);

            cooldown = 0.5f;
        } 
    }
}