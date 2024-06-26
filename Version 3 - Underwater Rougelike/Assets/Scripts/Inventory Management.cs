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
            items[i] = -1; // Begins with no items
        }
    }

    public void AddItem(int slotIndex, int itemID)
    {
        if (slotIndex >= 0 && slotIndex < slots.Length)
        {
            items[slotIndex] = itemID;
            Item item = itemDatabase.GetItemByID(itemID);

            if (item != null)
            {
                Image slotImage = slots[slotIndex].GetComponent<Image>();
                slotImage.sprite = item.itemIcon;
                slotImage.color = Color.white;
            }
        }
    }

    public void RemoveItem(int slotIndex)
    {
        if (slotIndex >= 0 && slotIndex < slots.Length)
        {
            items[slotIndex] = -1;
            Image slotImage = slots[slotIndex].GetComponent<Image>();
            slotImage.sprite = null;
            slotImage.color = new Color(1, 1, 1, 0); // Makes the slot transparent
        }
    }
}
