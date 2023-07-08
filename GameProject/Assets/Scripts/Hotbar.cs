using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.EventSystems;

public class Hotbar : MonoBehaviour {
    // Start is called before the first frame update
    public LevelEditor levelEditor;
    public HotbarSlot[] slots;


    private void Awake()
    {
        levelEditor = FindObjectOfType<LevelEditor>();
        slots = GetComponentsInChildren<HotbarSlot>();
    }


    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < slots.Length; i++)
        {
            Placeable p = levelEditor.hotbarredPlaceables.ElementAtOrDefault(i);
            if(p != null)
            {
                slots[i].UpdateSlot(p);
                if(p == levelEditor.activePlaceable)
                {
                    slots[i].ShowActive();
                }
            } else
            {
                slots[i].EmptySlot();
            }
        }
        
    }
}
