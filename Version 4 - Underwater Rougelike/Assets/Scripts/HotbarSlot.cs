using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HotbarSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public int slotIndex;
    private Hotbar hotbar;

    void Start()
    {
        hotbar = GameObject.FindObjectOfType<Hotbar>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (hotbar != null)
        {
            hotbar.ShowItemDescription(slotIndex);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (hotbar != null)
        {
            hotbar.ShowItemDescription(-1);
        }
    }
}
