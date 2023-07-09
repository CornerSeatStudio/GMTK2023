using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Repeater : MonoBehaviour
{
    // Start is called before the first frame update
    public float movespeed;
    public float waitTime;
    public bool delay;
    public bool dontspawn;
    public GameObject otherAlley;
    public GameObject marker;
    public bool movetrue;
    public bool isntBall;
    void Start()
    {

        
 
       
    }

    // Update is called once per frame
    void Update()
    {
        if (!isntBall)
        {
            transform.position -= new Vector3(movespeed * Time.deltaTime, 0f, 0f);
            if (transform.position.x < marker.transform.position.x)
            {
                transform.position = otherAlley.gameObject.transform.position + new Vector3(55.42f, 0f, 0f);
            }

        }

        if (movetrue)
        {
            transform.position += new Vector3(movespeed * Time.deltaTime, 0f, 0f);
        }
    }

  

    public void StopMoving()
    {
        movespeed = 0f;
        waitTime = 10000f;
        
    }

    public void StartMoving()
    {
        movetrue = true;
    }
}
//55.46f
