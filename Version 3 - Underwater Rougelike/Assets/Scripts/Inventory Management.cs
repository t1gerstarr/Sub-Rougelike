using UnityEngine.UI;
using UnityEngine;

public class InventoryManagement : MonoBehaviour
{
    public GameObject[] slots; // Array of slots
    private int[] items; // Array to store item IDs
    private ItemDatabase itemDatabase; // Reference to the ItemDatabase

    void Start()
    {
        itemDatabase = GameObject.FindObjectOfType<ItemDatabase>();

        items = new int[slots.Length];
        for (int i = 0; i < slots.Length; i++)
        {
            items[i] = -1; // Initialize with no item
        }

        // Update inventory UI upon load
        UpdateInventoryUI();
    }

    public void AddItem(int slotIndex, int itemID)
    {
        if (slotIndex >= 0 && slotIndex < slots.Length)
        {
            items[slotIndex] = itemID; // Assign the itemID to the specific slot
            UpdateInventoryUI(); // Update the UI to reflect the change
        }
        else
        {
            Debug.LogError($"Slot index '{slotIndex}' is out of bounds.");
        }
    }

    public void RemoveItem(int slotIndex)
    {
        if (slotIndex >= 0 && slotIndex < slots.Length)
        {
            items[slotIndex] = -1; // Remove the item from the specific slot
            UpdateInventoryUI(); // Update the UI to reflect the change
        }
    }

    private void UpdateInventoryUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            Transform itemIconTransform = slots[i].transform.Find("ItemIcon");
            if (itemIconTransform != null)
            {
                Image itemIconImage = itemIconTransform.GetComponent<Image>();
                if (itemIconImage != null)
                {
                    if (items[i] != -1)
                    {
                        Item item = itemDatabase.GetItemByID(items[i]);
                        if (item != null)
                        {
                            itemIconImage.sprite = item.itemIcon;
                            itemIconImage.color = Color.white; // Ensure the image is visible
                            Debug.Log($"Updated slot '{slots[i].name}' with item '{item.itemName}' (ID: {items[i]})");
                        }
                        else
                        {
                            Debug.LogError($"Item with ID '{items[i]}' not found in the database.");
                            itemIconImage.sprite = null;
                            itemIconImage.color = new Color(1, 1, 1, 0); // Make the item icon transparent
                        }
                    }
                    else
                    {
                        itemIconImage.sprite = null;
                        itemIconImage.color = new Color(1, 1, 1, 0); // Make the item icon transparent
                    }
                }
                else
                {
                    Debug.LogError($"Image component not found on ItemIcon child of slot '{slots[i].name}'.");
                }
            }
            else
            {
                Debug.LogError($"ItemIcon child not found in slot '{slots[i].name}'.");
            }
        }
    }
}
