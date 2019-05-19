using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
 {

    [SerializeField]

    private Stat health;
    private bool isDead = false;
    
    private float iniHealth = 100;

    //Rigidbody rb;

    // Use this for initialization
    void Start ()
    {
        health.Initialise(iniHealth, iniHealth); //Ensures start of game health is max. 
        //rb = GetComponent<Rigidbody>();

    }
	
	// Update is called once per frame
	void Update () {


        

		
	}

   public void TakeDamage(float damage)
    {
        health.MyCurrentValue -= damage;

        if (health.MyCurrentValue <= 0 && isDead == false)
        {
            Debug.Log("ARGH I'm Dead!");
            isDead = true;
        }
    }
}

