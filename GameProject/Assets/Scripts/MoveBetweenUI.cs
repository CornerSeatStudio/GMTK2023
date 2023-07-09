using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBetweenUI : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject otherUI;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwitchUI()
    {
        otherUI.SetActive(true);
        gameObject.SetActive(false);
    }
}
