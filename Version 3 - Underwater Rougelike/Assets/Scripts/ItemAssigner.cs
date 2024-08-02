using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAssigner : MonoBehaviour
{
    public Hotbar hotbar;
    public Sprite yoursprite;

    void Start()
    {
        Item newItem = new Item();
        newItem.itemID = 1;
        newItem.itemName = "Bubbles";
        newItem.itemIcon = yoursprite;
        newItem.itemDescription = "Compressed Air.";

        hotbar.AssignItemToSlot(1, newItem);
    }
}
