using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlotController : MonoBehaviour
{

    public Item item;

    public void UpdateInfo() //Gets and updates the components with item details
    {

        Text displayText = transform.Find("Text").GetComponent<Text>();
        Image displayImage = transform.Find("Image").GetComponent<Image>();

        if (item) //Only true if item in slot.
        {
            displayText.text = item.itemName;
            displayImage.sprite = item.icon;
        }

        else
        {
            displayText.text = "";
            displayImage.sprite = null;
            //displayImage.color = Color.clear;
        }



    }

    public void Use()
    {
        if (item)
        {
            item.Use();
        }
    }

    private void Start()
    {
        UpdateInfo();
    }

}
