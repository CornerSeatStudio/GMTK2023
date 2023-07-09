using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System.Linq;
using UnityEngine.SceneManagement;
public class CountUprightCylinders : MonoBehaviour
{

    public float noPinsLeftTimeout = 5f;

    private List<Pin> uprightPins;
    private List<Pin> fallenPins; //todo may use in future
    private BallMovement ball;
    public GameObject WinModeUI;
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
    IEnumerator PinLoop()
    {
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

        //at this point, no pins left standing
        yield return new WaitForSeconds(noPinsLeftTimeout);
        OnWin(); //todo this might trigger twice, once at all pins down, second in gutter

    }



    void OnTriggerEnter(Collider other)
    {

        //ball in gutter, check for win
        if (other.gameObject.CompareTag("Ball")) 
        {
            CheckPins();
        }
    }


    void OnWin()
    {
        Debug.Log("all pins down");
        WinModeUI.SetActive(true);
        //scene management here
    }
    void nextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    void CheckPins()
    { 

        if (uprightPins.Count == 0)
        {
            OnWin();
        }
        else
        {
            Debug.Log("Uprigh remaining" + uprightPins.Count);
        }
    }
}
