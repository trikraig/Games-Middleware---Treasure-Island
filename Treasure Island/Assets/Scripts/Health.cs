using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour {

    public Slider healthbar;
    public float maxHealth;
    public float currentHealth; //Public for testing purposes

	// Use this for initialization
	void Start () {
        
        currentHealth = maxHealth;
		
	}


   

    public void Heal(float amount) //Increases health by amount passed in. Ensures new current health does not exceed max health.
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        if (healthbar == null)
        {
            healthbar = Inventory.instance.GetComponentInChildren<Slider>();
        }
        else
        {
            healthbar.value = currentHealth / maxHealth;
        }
        
    }
    
    public void IncreaseMaxHealth(float amount)
    {
        maxHealth += amount; //Increases maximum health by amount passed in.
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;

        healthbar.value = currentHealth / maxHealth;

        if (currentHealth <= 0)
        {

            SendMessage("onDeath"); //Tells every component on the game object to run the onDeath function.
        }

        
        
        else //Only play hurt animation if not dead.
        {
            Animator anim = this.GetComponent<Animator>();

            if (anim)
            {
                anim.SetTrigger("Hurt");
            }
        }
        
    }

    

    public void Reset() //Resets the game state to restore health.
    {
        currentHealth = maxHealth;
        healthbar.value = currentHealth / maxHealth;
    }
}
