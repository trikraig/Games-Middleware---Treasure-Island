using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Equipment", menuName = "Items/Equipment")] //Class attribute which allows the creation of Equipment in the inspector.

public class Equipment : Item { //Inherits from item class.

   //Will not equip sword on player until player clicks button.

    public override void Use()
    {

        Debug.Log("Use Item");
        //Access the player component via the inventory instance.

        GameObject player = Inventory.instance.player;


        playerController pC = player.GetComponent<playerController>();

        if (itemName == "Sword")
        {
            pC.equipSword();
        }

        else if (itemName == "Gun")
        {
            pC.equipGun();
        }



    }
}
