using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour { 
    
    //Needs Add and Remove Methods.

    public List<Item> list = new List<Item>();  //Linked list to hold the current inventory.
    public GameObject player;
    public GameObject inventoryPanel;

    public static Inventory instance; //Static singular instance of the inventory that can be accessed by any other script.

    void UpdatePanelSlots()
    {
        int index = 0; //Index to count which slot number in during loop.
        foreach (Transform child in inventoryPanel.transform) //Iterates through slots in inventory panel.
        {
            InventorySlotController slot = child.GetComponent<InventorySlotController>();
            if (index < list.Count)
            {
                slot.item = list[index]; //If containts item at index, sets the slots item to the item at list [index].
            }
            else
            {
                slot.item = null; //if does not contain an item.
            }
            slot.UpdateInfo();
            index++;
        }


    }
    
    public void Add(Item item) //Adds item to inventory.
    {
        if (list.Count < 8) //Total slots in inventory.
        {
            list.Add(item);
            Debug.Log("Item added to inventory");
        }

        UpdatePanelSlots();
    }

    public void Remove(Item item) //Removes item from inventory.
    {
        list.Remove(item);
        Debug.Log("Item removed from inventory");
        UpdatePanelSlots();
    }

    public bool HasKey()
    {
        Debug.Log("Checking for key");

        int index = 0;

        foreach (Transform child in inventoryPanel.transform) //Iterates through slots in inventory panel.
        {
            InventorySlotController slot = child.GetComponent<InventorySlotController>();
            if (slot.item != null)
            {
                if (slot.item.name == "Key")
                {
                    Remove(slot.item);
                    return true;
                }

            }
            
            
            slot.UpdateInfo();
            index++;
        }

        return false;

    }

    public bool HasMapAndCompass()
    {
        int index = 0;
        bool hasCompass = false;
        bool hasMap = false;

        foreach (Transform child in inventoryPanel.transform) //Iterates through slots in inventory panel.
        {
            InventorySlotController slot = child.GetComponent<InventorySlotController>();
            if (slot.item != null)
            {
                if (slot.item.name == "Compass")
                {
                    hasCompass = true;
                    
                }

                if (slot.item.name == "Treasure Map")
                {
                    hasMap = true;
                    
                }

            }


            slot.UpdateInfo();
            index++;
        }

        if (hasCompass && hasMap)
        {
            return true;
        }
        else
        {
            return false;
        }

    }

    // Use this for initialization
    void Start ()
    {
        instance = this;
        
        UpdatePanelSlots();
	}

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (player == null)
        {
            player = GameObject.FindWithTag("Player");
        }

    }

        

    

    
}
