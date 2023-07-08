using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class StartMove : MonoBehaviour
{
    public Rigidbody rb;
    public float launchForce=10f;
    public float spinForce=1f;
    public float collisionForce = 2f;
    public float gutterForce = 10f;
 //   public CinemachineVirtualCamera virtualCamera;
    public GameObject ball;
    public GameObject EditModeUI;
    public GameObject PlayModeUI;
    public bool launched = false;
    public CameraTargetBevahior cameraTarget;

    private LevelEditor levelEditor;
    private ObjectResetter resetter;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        levelEditor = FindObjectOfType<LevelEditor>();
        resetter = FindObjectOfType<ObjectResetter>();
    }

    void Start()
    {

        rb.isKinematic = true;
    }



    public void OnSendLeBall()
    {
        //are all pins placed?
        if (levelEditor != null && !levelEditor.AreAllPinsPlaced())
        {
            Debug.Log("NOT ALL PINS ARE PLACED, FLASH A UI THING HERE");

            return;
        }

        //free the lad
        rb.isKinematic = false;

        //boot him away
        Vector3 torqueDirection = transform.right;
        rb.AddForce(Vector3.forward * launchForce, ForceMode.Impulse);
        rb.AddTorque(torqueDirection * spinForce, ForceMode.Impulse);


        //cameraTarget.SetToTarget(gameObject);
        launched = true;
        if (EditModeUI != null)
        {
            EditModeUI.SetActive(false);
        }
        if (PlayModeUI != null)
        {
            PlayModeUI.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!launched && Input.GetKeyDown(KeyCode.Space))
        {
            if (resetter != null)
            {
                resetter.OnPlay();
            }
            OnSendLeBall();
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Gutter")
        {
            Vector3 initialGutterForce = new Vector3(-1, -1, 0) * collisionForce;
            rb.AddForce(initialGutterForce, ForceMode.Impulse);
            Debug.Log("In");
        }
    }
    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Gutter")
        {
            Vector3 constantGutterForce = new Vector3(0, 0, 1) * gutterForce; 
            rb.AddForce(constantGutterForce);
        }
    }
}
