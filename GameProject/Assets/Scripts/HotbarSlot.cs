using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class HotbarSlot : MonoBehaviour, IPointerClickHandler
{
    public Hotbar hotbarParent;

    public Placeable placeable;
    private Image image;
    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button != 0 || placeable == null)
        {
            return;
        }

        if(placeable == hotbarParent.levelEditor.activePlaceable)
        {
            //BUG HERE
            Debug.Log("BUG?");
            hotbarParent.levelEditor.ActiveToInactive();
        } else
        {
            hotbarParent.levelEditor.XToActive(placeable);
        }
        
    }

    private void Awake()
    {
        hotbarParent = GetComponentInParent<Hotbar>();
        image = GetComponent<Image>();  
    }

    public void UpdateSlot(Placeable p)
    {
        placeable = p;
        //p.spriteIcon;
        image.sprite = p.spriteIcon;
        
    }

    public void EmptySlot()
    {
        placeable = null;
        image.sprite = null;

    }

    public void ShowActive()
    {

    }
}
