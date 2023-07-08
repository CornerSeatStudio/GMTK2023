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
            isUpright uprightCheck = Pin.GetComponent<isUpright>();

            if (uprightCheck!=null&&uprightCheck.IsUpright())
            {
                uprightCount++;
            }
        }
        Debug.Log("Upright: " + uprightCount);
    }
}
