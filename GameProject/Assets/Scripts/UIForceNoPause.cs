using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIForceNoPause : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject winConMenu;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(winConMenu.activeInHierarchy){
            pauseMenu.SetActive(false);
        }
    }
}
