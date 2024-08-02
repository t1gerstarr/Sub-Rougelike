using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Animations;
using UnityEditorInternal;
using UnityEngine;

public class Slot : MonoBehaviour
{
    private Inventory inventory;
    public int i;

    private void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
    }

    private void Update()
    {
        if (transform.childCount <= 0)
        {
            inventory.isFull[i] = false;
        }
    }
    public void DropItem()
    {
        // Create a list to store the children
        List<GameObject> children = new List<GameObject>();

        // Iterate through each child and add them to the list
        foreach (Transform child in transform)
        {
            children.Add(child.gameObject);
        }

        // Destroy each child GameObject
        foreach (GameObject child in children)
        {
            child.GetComponent<Spawn>().SpawnDroppedItem();
            Destroy(child);
        }
    }
}
