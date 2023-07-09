using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOnObject : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject target;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void TurnOnMenu()
    {
        target.SetActive(true);
    }

    public void TurnOffMenu()
    {
        target.SetActive(false);
    }
}
