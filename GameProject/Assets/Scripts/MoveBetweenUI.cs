using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBetweenUI : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject otherUI;
    public GameObject sound;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwitchUI()
    {
        Instantiate(sound, Vector3.zero, Quaternion.identity);
        otherUI.SetActive(true);
        gameObject.SetActive(false);
    }
}
