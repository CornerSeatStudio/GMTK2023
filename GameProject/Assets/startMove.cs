using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startMove : MonoBehaviour
{
    public Rigidbody rb;
    public float launchForce=10f;
    public float spinForce=1f;
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

            // Apply a force to the object to the right.
            Vector3 torqueDirection = transform.right;
            rb.AddForce(Vector3.forward * launchForce, ForceMode.Impulse);
            rb.AddTorque(torqueDirection * spinForce, ForceMode.Impulse);
        }
    }
}
