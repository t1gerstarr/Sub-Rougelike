using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[System.Serializable]
public class Hotbar : MonoBehaviour
{
    public GameObject[] slots;
    public Text descriptionText;
    private int currentSlotIndex;

    void Start()
    {
        UpdateHotbarUI();
        SelectSlot(0);
    }

    void Update()
    {
        HandleInput();
    }

    void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) SelectSlot(0);
        if (Input.GetKeyDown(KeyCode.Alpha2)) SelectSlot(1);
        if (Input.GetKeyDown(KeyCode.Alpha3)) SelectSlot(2);
    }

    void SelectSlot(int index)
    {
        if (index >= 0 && index < slots.Length)
        {
            currentSlotIndex = index;
            UpdateHotbarUI();
        }
    }

    void InitializeSlots()
    {
        for (int i = 1; i < slots.Length; i++)
        {
            Transform itemIconTransform = slots[i].transform.Find("ItemIcon");
            if (itemIconTransform != null)
            {
                Image itemIconImage = itemIconTransform.GetComponent<Image>();
                if (itemIconImage != null)
                {
                    itemIconImage.enabled = false;
                }
            }
        }
        UpdateHotbarUI();
    }

    void UpdateHotbarUI()
    {
       for (int i = 0; i < slots.Length; i++)
        {
            Transform itemIconTransform = slots[i].transform.Find("ItemIcon");
            if (itemIconTransform != null)
            {
                Image itemIconImage = itemIconTransform.GetComponent<Image>();
                Item item = itemIconTransform.GetComponent<Item>();

                if (itemIconImage != null)
                {
                    if (item != null)
                    {
                        itemIconImage.sprite = item.itemIcon;
                        itemIconImage.color = Color.white;
                        itemIconImage.enabled = true; // Enable the Image component if an item is assigned
                    }
                    else
                    {
                        itemIconImage.sprite = null;
                        itemIconImage.color = new Color(1, 1, 1, 0);
                        itemIconImage.enabled = false; // Disable the Image component if no item is assigned
                    }
                }
            }
        }
    }

    public void ShowItemDescription(int slotIndex)
    {
        if (slotIndex >= 0 && slotIndex < slots.Length)
        {
            Transform itemIconTransform = slots[slotIndex].transform.Find("ItemIcon");
            Item item = itemIconTransform.GetComponent<Item>();

            if (item != null)
            {
                descriptionText.text = item.itemDescription;
            }
            else
            {
                descriptionText.text = "";
            }
        }
    }

    public void AssignItemToSlot(int slotIndex, Item newItem)
    {
        if (slotIndex >= 0 && slotIndex < slots.Length)
        {
            Transform itemIconTransform = slots[slotIndex].transform.Find("ItemIcon");
            if(itemIconTransform != null)
            {
                Item item = itemIconTransform.GetComponent<Item>();
                if (item != null)
                {
                    item.itemID = newItem.itemID;
                    item.itemName = newItem.itemName;
                    item.itemIcon = newItem.itemIcon;
                    item.itemDescription = newItem.itemDescription;

                    Image itemIconImage = itemIconTransform.GetComponent<Image>();
                    if (itemIconImage != null)
                    {
                        itemIconImage.sprite = newItem.itemIcon;
                        itemIconImage.enabled = true;
                    }
                }
            }
            UpdateHotbarUI();
        }
    }
}
