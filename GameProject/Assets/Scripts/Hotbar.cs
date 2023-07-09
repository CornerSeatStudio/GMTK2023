using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.EventSystems;

public class Hotbar : MonoBehaviour {
    // Start is called before the first frame update
    private RectTransform rTransform;
    public LevelEditor levelEditor;
    public HotbarSlot[] slots;
    public Sprite selectedSlot;
    public Sprite unselectedSlot;
    public float swiftyOffset = 60f;
    public float swiftySpeed = 5f;

    private void Awake()
    {
        rTransform = GetComponent<RectTransform>(); 
        levelEditor = FindObjectOfType<LevelEditor>();
        slots = GetComponentsInChildren<HotbarSlot>();
    }


    private void Start()
    {
        //foreach(HotbarSlot slot in slots)
        //{
        //    slot.SetInactiveFrame();
        //}

        StartCoroutine(CoolMove());
    }

    IEnumerator CoolMove()
    {
        float t = 0;
        Vector3 initPos = rTransform.localPosition;
        rTransform.localPosition -= Vector3.right * swiftyOffset;
        while(t < 1f)
        {
            rTransform.localPosition = Vector3.Lerp(rTransform.localPosition, initPos, Time.deltaTime * swiftySpeed);
            t += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        rTransform.localPosition = initPos;
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
                    slots[i].SetActiveFrame();
                } else
                {
                    slots[i].SetInactiveFrame();
                }


            } else
            {
                slots[i].EmptySlot();
                slots[i].SetInactiveFrame();

            }
        }
        
    }
}
