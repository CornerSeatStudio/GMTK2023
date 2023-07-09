using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System.Linq;
using UnityEngine.SceneManagement;
public class CountUprightCylinders : MonoBehaviour
{

    public float noPinsLeftTimeout = 2f;

    private List<Pin> uprightPins;
    private List<Pin> fallenPins; //todo may use in future
    private BallMovement ball;
    private ObjectResetter resetter;
    public GameObject WinModeUI;
    public RectTransform StrikeUI;
    public GameObject winSound;
    public GameObject clapping;

    public AudioClip[] niceThrows;
    public AudioClip[] strikes;

    public float StrikeUIOffset = 400f;
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
        Time.timeScale = 1f;
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
            List<Pin> newFallenPins = uprightPins.Where(item => !item.IsUpright()).ToList(); // check for fallen pins
            if (newFallenPins != null) { 
                uprightPins.RemoveAll(item => !item.IsUpright());
                fallenPins.AddRange(newFallenPins);
            }

            //check for pins that miraculously stand up on their own again
            List<Pin> newUprightPins = fallenPins.Where(item => item.IsUpright()).ToList(); // check for fallen pins
            if (newUprightPins != null) { 
                fallenPins.RemoveAll(item => item.IsUpright());
                uprightPins.AddRange(newUprightPins);
            }
            yield return new WaitForEndOfFrame();
        }

        won = true;
        if (ball.PlayModeUI != null)
        {
            ball.PlayModeUI.SetActive(false);
        }
        

        ball.cameraTarget.SetToTarget(null); //dont snap camera

        float t = 0;
        Vector3 targetPos = StrikeUI.localPosition + Vector3.down * StrikeUIOffset;

        AudioSource[] sources = StrikeUI.GetComponents<AudioSource>();
        AudioClip[] soundChoice = UnityEngine.Random.value > 0.5f ? strikes : niceThrows;
        for(int i = 0; i < soundChoice.Length; ++i)
        {
            sources[i].PlayOneShot(soundChoice[i]);
        }

        while (t < noPinsLeftTimeout)
        {
            StrikeUI.localPosition = Spring(StrikeUI.localPosition, targetPos, t * .1f);
            t += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        //at this point, no pins left standing

        if(ball.launched) //early reset dumbass
            OnWin(); 

    }

    float Spring(float from, float to, float time)
    {
        time = Mathf.Clamp01(time);
        time = (Mathf.Sin(time * Mathf.PI * (.2f + 2.5f * time * time * time)) * Mathf.Pow(1f - time, 2.2f) + time) * (1f + (1.2f * (1f - time)));
        return from + (to - from) * time;
    }

    Vector3 Spring(Vector3 from, Vector3 to, float time)
    {
        return new Vector3(Spring(from.x, to.x, time), Spring(from.y, to.y, time), Spring(from.z, to.z, time));
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
        Instantiate(clapping, Vector3.zero, Quaternion.identity);
        WinModeUI.SetActive(true);
        Time.timeScale = 0f;
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
