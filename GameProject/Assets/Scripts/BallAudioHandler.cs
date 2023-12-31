using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BallMovement))]
public class BallAudioHandler : MonoBehaviour
{
    public AudioClip rollStart;
    public AudioClip rollDuring;
    //public AudioClip bangPin;
    public Vector2 pitchTickRange = new(0.7f, 1.5f);
    private AudioSource m_AudioSource;
    private BallMovement ball;
    private Rigidbody rb;


    private void Awake()
    {
        ball = GetComponent<BallMovement>();
        rb = GetComponent<Rigidbody>();
        m_AudioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        m_AudioSource.loop = true;
    }

    private void OnEnable()
    {
        ball.OnPlayEvent += OnPlay;
    }

    private void OnDisable()
    {
        ball.OnPlayEvent -= OnPlay;

    }

    IEnumerator audioRoutine;
    private void Update()
    {

        if (!ball.launched) //todo threshold to stop sound
        {
            if(audioRoutine != null)
            {
                StopCoroutine(audioRoutine);
                m_AudioSource.Stop();
                audioRoutine = null;

            }
        }

    }

    void OnPlay()
    {
        audioRoutine = AudioRoutine();
        StartCoroutine(audioRoutine);
    }

    IEnumerator AudioRoutine()
    {
        m_AudioSource.PlayOneShot(rollStart);
        yield return new WaitForSeconds(1.5f);
        m_AudioSource.clip = rollDuring; //todo scale volume over time?
        m_AudioSource.Play();
    }





}
