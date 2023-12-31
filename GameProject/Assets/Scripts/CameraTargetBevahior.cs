using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTargetBevahior : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject trackingTarget;
    public GameObject leftBound;
    public GameObject rightBound;
    public GameObject upperBound;
    public GameObject lowerBound;
    public float movespeed;
    private Vector3 startPos;
    public bool isOrtho = false;
    public GameObject background;

    void Awake()
    {
        startPos = transform.position;
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
            if (Input.GetKey(KeyCode.W))
            {
                
                if (transform.position.x > upperBound.transform.position.x)
                {
                    
                    transform.position -= new Vector3(movespeed * Time.deltaTime, 0f, 0f);
                }
            }
            if (Input.GetKey(KeyCode.S))
            {
                
                if (transform.position.x < lowerBound.transform.position.x)
                {
                    
                    transform.position += new Vector3(movespeed * Time.deltaTime, 0f, 0f);
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ToggleOrtho();
        }
    }
    public void SetToTarget(GameObject target){
        trackingTarget=target;
    }

    public void RemoveTarget()
    {
        trackingTarget = null;
        transform.position = startPos;
    }

    public void ToggleOrtho()
    {
        Camera.main.orthographic = !isOrtho;
        isOrtho = !isOrtho;
        if (isOrtho)
        {
            transform.rotation = Quaternion.Euler(35f, -120f, 0f);
            background.transform.localScale = new Vector3(1.5f, 9f, 9f);
        }
        else
        {
            transform.rotation = Quaternion.Euler(35f, -90f, 0f);
            background.transform.localScale = new Vector3(4.34f, 9f, 9f);
        }
    }
}
