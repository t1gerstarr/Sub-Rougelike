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
                    }
                    else
                    {
                        itemIconImage.sprite = null;
                        itemIconImage.color = Color.clear;
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
}
