using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "new Consumable", menuName = "Items/Consumable")] //Class attribute which allows the creation of Consumables in the inspector.

public class Consumable : Item { //Inherits from item class.

    public int heal = 0; //Default consumable values;
    public int maxHealthUp = 0;
    public bool isConsumable = true;

    public override void Use()
    {
        GameObject player = Inventory.instance.player; //Access the player component via the inventory instance.
        Health playerHealth = player.GetComponent<Health>();//Player's health component reference.

        playerHealth.Heal(heal); //Heals player by passed amount.
        playerHealth.IncreaseMaxHealth(maxHealthUp); //Increases the players max health.

        if (itemName == "Gold")
        {
            FindObjectOfType<LevelChanger>().FadeToQuit();
        }

        if (isConsumable)
        {
            Inventory.instance.Remove(this);//Removes used consumable from inventory.
        }
    }

}
