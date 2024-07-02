using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IPointerClickHandler
{
    public int slotIndex; // Index of the slot within Inventory
    private InventoryManagement inventory;

    void Start()
    {
        inventory = GameObject.FindObjectOfType<InventoryManagement>();
    }   

    public void OnPointerClick(PointerEventData eventData)
    {
        if (inventory != null)
        {
            inventory.AddItem(slotIndex, 1); // adds item with ID 1
        }
    }

}