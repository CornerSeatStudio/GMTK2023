using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startMove : MonoBehaviour
{
    public Rigidbody rb;
    public float launchForce=10f;
    public float spinForce=1f;
    public float collisionForce = 2f;
    public float gutterForce = 10f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        transform.position = new Vector3(transform.position.x, 1, transform.position.z);
        rb.useGravity = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.useGravity = true;

            Vector3 torqueDirection = transform.right;
            rb.AddForce(Vector3.forward * launchForce, ForceMode.Impulse);
            rb.AddTorque(torqueDirection * spinForce, ForceMode.Impulse);
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
