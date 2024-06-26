using UnityEngine;

[System.Serializable]
public class Item
{
    public int itemID;
    public string itemName;
    public Sprite itemIcon;
}

public class ItemDatabase : MonoBehaviour
{
    public Item[] items;

    public Item GetItemByID(int id)
    {
        foreach (Item item in items)
        {
            if (item.itemID == id)
                return item;
        }
        return null;
    }
}
