using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSound : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject sound;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MakeSound()
    {
        Instantiate(sound, Vector3.zero, Quaternion.identity);
    }
}
