using UnityEngine.UI;
using UnityEngine;

public class InventoryManagement : MonoBehaviour
{
    public GameObject[] slots; // Creates the array of slots for items
    private int[] items; // Stores the Items ID's
    private ItemDatabase itemDatabase; // reference to the ItemDataBase script

    void Start()
    {
        itemDatabase = GameObject.FindObjectOfType<ItemDatabase>(); // finds the itemdatabase in the scene

        items = new int[slots.Length];

        for (int i = 0; i < slots.Length; i++)
        {
            items[i] = 0; // Begins with no items
        }

        UpdateInventoryUI();
    }

    public void AddItem(int slotIndex, int itemID)
    {
        if (slotIndex >= 0 && slotIndex < slots.Length)
        {
            items[slotIndex] = itemID;
            UpdateInventoryUI();
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
            items[slotIndex] = -1;
            UpdateInventoryUI();
        }
    }

    private void UpdateInventoryUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            Image slotImage = slots[i].GetComponent<Image>();
            if (items[i] != -1)
            {
                Item item = itemDatabase.GetItemByID(items[i]);
                if (item != null)
                {
                    slotImage.sprite = item.itemIcon;
                    slotImage.color = Color.white;
                }
                else
                {
                    Debug.LogError($"Item with ID '{items[i]}' not found in the database");
                    slotImage.sprite = null;
                    slotImage.color = new Color(1, 1, 1, 0);
                }
            }
            else
            {
                slotImage.sprite = null;
                slotImage.color = new Color(1, 1, 1, 0);
            }
        }
    }
}
