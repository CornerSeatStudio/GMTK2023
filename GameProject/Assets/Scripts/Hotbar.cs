using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hotbar : MonoBehaviour {
    // Start is called before the first frame update
    public LevelEditor levelEditor;
    private void Awake()
    {
        levelEditor = FindObjectOfType<LevelEditor>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
