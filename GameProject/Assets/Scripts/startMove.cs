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
    private bool following = false;
    private bool launched = false;
    public CameraTargetBevahior cameraTarget;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        rb.isKinematic = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!launched && Input.GetKeyDown(KeyCode.Space))
        {
            rb.isKinematic = false;

            Vector3 torqueDirection = transform.right;
            rb.AddForce(Vector3.forward * launchForce, ForceMode.Impulse);
            rb.AddTorque(torqueDirection * spinForce, ForceMode.Impulse);
            if (!following && Input.GetKeyDown(KeyCode.Space))
        {
                // virtualCamera.Follow = ball.transform;
                // virtualCamera.LookAt = ball.transform;
                cameraTarget.SetToTarget(gameObject);
                following = true;
                launched = true;
                EditModeUI.SetActive(false);
                PlayModeUI.SetActive(true);
            }
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
