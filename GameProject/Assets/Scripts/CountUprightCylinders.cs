using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class CountUprightCylinders : MonoBehaviour
{
    private bool countdownStarted=false;
    private float countdownTimer=5f;
    public CinemachineVirtualCamera virtualCamera;
    void Update()
    {
        if (countdownStarted)
        {
            countdownTimer-=Time.deltaTime;
            if (countdownTimer<=0)
            {
                CheckPins();
                countdownStarted=false;
            }
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ball")) 
        {
            countdownStarted=true;
            countdownTimer=5f;
            virtualCamera.Follow=null;
            virtualCamera.LookAt=null;
        }
    }

    void CheckPins()
    {
        GameObject[] pins=GameObject.FindGameObjectsWithTag("Pins");

        int uprightCount=0;

        foreach (GameObject pin in pins)
        {
            IsUpright uprightCheck=pin.GetComponent<IsUpright>();

            if (uprightCheck!=null && uprightCheck.CheckUpright())
            {
                uprightCount++;
            }
        }

        if (uprightCount==0)
        {
            Debug.Log("Down");
        }
        else
        {
            Debug.Log("Upright: " + uprightCount);
        }
    }
}
