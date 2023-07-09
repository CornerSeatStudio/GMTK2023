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
    private Image frameImage;
    public Image iconImage;
    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button != 0 || placeable == null)
        {
            return;
        }

        hotbarParent.levelEditor.AudioSource.PlayOneShot(hotbarParent.levelEditor.OnSelect);

        if(placeable == hotbarParent.levelEditor.activePlaceable)
        {
            //BUG HERE
            //Debug.Log("BUG?");
            hotbarParent.levelEditor.ActiveToInactive();
        } else
        {
            hotbarParent.levelEditor.XToActive(placeable);
        }
        
    }

    private void Awake()
    {
        hotbarParent = GetComponentInParent<Hotbar>();
        frameImage = GetComponent<Image>();

    }

    public void UpdateSlot(Placeable p)
    {
        placeable = p;
        //p.spriteIcon;
        iconImage.sprite = p.spriteIcon;
        
    }

    public void EmptySlot()
    {
        placeable = null;
        iconImage.sprite = null;

    }

    public void SetActiveFrame()
    {
        if(frameImage.sprite != hotbarParent.selectedSlot)
            frameImage.sprite = hotbarParent.selectedSlot;
    }

    public void SetInactiveFrame()
    {
        if (frameImage.sprite != hotbarParent.unselectedSlot)
            frameImage.sprite = hotbarParent.unselectedSlot;

    }



}
