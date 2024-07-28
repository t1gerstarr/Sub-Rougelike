using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hotbar : MonoBehaviour
{
    public GameObject[] slots;
    public Item[] items;
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
                if (itemIconImage != null)
                {
                    if (items[i] != null)
                    {
                        itemIconImage.sprite = items[i].itemIcon;
                        itemIconImage.color = Color.white;
                    }
                    else
                    {
                        itemIconImage.sprite = null;
                        itemIconImage.color = new Color(1, 1, 1, 0);
                    }
                }
            }
        }
    }

    public void ShowItemDescription(int slotIndex)
    {
        if (slotIndex >= 0 && slotIndex < items.Length && items[slotIndex] != null)
        {
            descriptionText.text = items[slotIndex].itemDescription;
        }
        else
        {
            descriptionText.text = "";
        }
    }
}
