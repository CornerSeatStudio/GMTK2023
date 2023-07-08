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

    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button != 0)
        {
            return;
        }

        if(placeable == hotbarParent.levelEditor.activePlaceable)
        {
            hotbarParent.levelEditor.ActiveToInactive();
        } else
        {
            hotbarParent.levelEditor.XToActive(placeable);
        }
        
    }

    private void Awake()
    {
        hotbarParent = GetComponentInParent<Hotbar>();
    }

    public void UpdateSlot(Placeable p)
    {
        placeable = p;
        //p.spriteIcon;
    }

    public void EmptySlot()
    {
        placeable = null;

    }

    public void ShowActive()
    {

    }
}
