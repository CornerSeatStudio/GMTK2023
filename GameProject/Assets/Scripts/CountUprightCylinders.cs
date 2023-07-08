using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountUprightCylinders : MonoBehaviour
{
    void Update()
    {
        GameObject[] Pins = GameObject.FindGameObjectsWithTag("Pins");

        int uprightCount = 0;

        foreach (GameObject Pin in Pins)
        {
            IsUpright uprightCheck = Pin.GetComponent<IsUpright>();

            if (uprightCheck!=null&&uprightCheck.CheckUpright())
            {
                uprightCount++;
            }
        }
        Debug.Log("Upright: " + uprightCount);
    }
}
