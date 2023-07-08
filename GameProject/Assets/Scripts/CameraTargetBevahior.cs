using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTargetBevahior : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject trackingTarget;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(trackingTarget){
            transform.position = trackingTarget.transform.position;
        }
    }
    public void SetToTarget(GameObject target){
        trackingTarget=target;
    }
}
