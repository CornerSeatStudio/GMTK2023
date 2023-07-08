using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(StartMove))]
public class BallAudioHandler : MonoBehaviour
{
    public AudioClip rollStart;
    public AudioClip rollDuring;
    public AudioClip bangPin;
    public float stopClipSpeedThreshold = 1f;
    private AudioSource m_AudioSource;
    private StartMove ball;
    private Rigidbody rb;


    private void Awake()
    {
        ball = GetComponent<StartMove>();
        rb = GetComponent<Rigidbody>();
        m_AudioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        m_AudioSource.loop = true;
    }
    bool IsRolling => ball.launched;
    bool audioTrigger = true;
    IEnumerator audioRoutine;
    private void Update()
    {
        if(!IsRolling || rb.velocity.sqrMagnitude < stopClipSpeedThreshold)
        {
            if(audioRoutine != null)
            {
                StopCoroutine(audioRoutine);
            }
            m_AudioSource.Stop();
            audioTrigger = true;
        } else if (audioTrigger)
        {
            StartCoroutine(AudioRoutine());
            audioTrigger = false;
        }
        
    }

    IEnumerator AudioRoutine()
    {
        m_AudioSource.PlayOneShot(rollStart);
        yield return new WaitForSeconds(1.5f);
        m_AudioSource.clip = rollDuring; //todo scale volume over time?
        m_AudioSource.Play();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.TryGetComponent<Pin>(out Pin pin))
        {
            pin.audioSouce.PlayOneShot(bangPin);
        }
    }
}
