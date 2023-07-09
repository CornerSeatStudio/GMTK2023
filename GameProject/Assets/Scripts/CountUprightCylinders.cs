using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System.Linq;
using UnityEngine.SceneManagement;
public class CountUprightCylinders : MonoBehaviour
{

    public float noPinsLeftTimeout = 1f;

    private List<Pin> uprightPins;
    private List<Pin> fallenPins; //todo may use in future
    private BallMovement ball;
    private ObjectResetter resetter;
    public GameObject WinModeUI;
    public GameObject winSound;
    private void OnEnable()
    {
        ball.OnPlayEvent += OnPlay;
    }

    private void OnDisable()
    {
        ball.OnPlayEvent -= OnPlay;
    }

    private void Awake()
    {
        ball = FindObjectOfType<BallMovement>();
        resetter = FindObjectOfType<ObjectResetter>();

    }

    void OnPlay() //triggers when ball launched
    {
        GameObject[] pins = GameObject.FindGameObjectsWithTag("Pins");
        uprightPins = pins.Select(pin => pin.GetComponent<Pin>()).ToList();
        fallenPins = new();

        if(pinLoop != null)
        {
            StopCoroutine(pinLoop);
        }

        pinLoop = PinLoop();
        StartCoroutine(pinLoop);

    }

    private IEnumerator pinLoop;
    private bool won = false;
    IEnumerator PinLoop()
    {
        won = false;
        while(uprightPins.Count > 0) //track pins when they fall down
        {
            List<Pin> newFallenPins = uprightPins.Where(item => !item.IsUpright()).ToList();
            if (newFallenPins != null) {
                newFallenPins.ForEach(pin => pin.audioSouce.Play());
                uprightPins.RemoveAll(item => !item.IsUpright());
                fallenPins.AddRange(newFallenPins);
            }

            yield return new WaitForEndOfFrame();
        }

        won = true;
        //at this point, no pins left standing
        yield return new WaitForSeconds(noPinsLeftTimeout);
        OnWin(); //todo this might trigger twice, once at all pins down, second in gutter

    }



    void OnTriggerEnter(Collider other)
    {
        if (won)
        {
            return;
        }

        //ball in gutter, check for win
        if (other.gameObject.CompareTag("Ball")) 
        {
            ResetPins();
        }
    }


    void OnWin()
    {
        Debug.Log("all pins down");
        Instantiate(winSound, Vector3.zero, Quaternion.identity);
        WinModeUI.SetActive(true);
        //scene management here
    }


    void ResetPins()
    {
        Debug.Log($"Upright remaining: {uprightPins.Count}, resetting");
        resetter.OnReset();

    }
    void nextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

}
