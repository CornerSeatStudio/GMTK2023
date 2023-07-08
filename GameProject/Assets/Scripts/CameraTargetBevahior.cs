using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTargetBevahior : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject trackingTarget;
    public GameObject leftBound;
    public GameObject rightBound;
    public float movespeed;
    public Transform startingTransform;

    void Awake()
    {
        startingTransform = transform;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(trackingTarget){
            transform.position = trackingTarget.transform.position;
        }
        else{
            if(Input.GetKey(KeyCode.A)){
                if(transform.position.z>leftBound.transform.position.z){
                    transform.position -= new Vector3(0f,0f,movespeed *Time.deltaTime);
                }
            }
            if (Input.GetKey(KeyCode.D))
            {
                if (transform.position.z < rightBound.transform.position.z)
                {
                    transform.position += new Vector3(0f, 0f, movespeed * Time.deltaTime);
                }
            }
        }
    }
    public void SetToTarget(GameObject target){
        trackingTarget=target;
    }

    public void RemoveTarget()
    {
        trackingTarget = null;
        transform.position = startingTransform.position;
        transform.rotation = startingTransform.rotation; 
    }
}
