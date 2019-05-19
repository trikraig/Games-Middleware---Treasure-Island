using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    [SerializeField]

    private Stat health;

     
    private float iniHealth = 100;


    // Use this for initialization
    void Start () {

        health.Initialise(iniHealth, iniHealth); //Ensures start of game health is max.
		
	}
	
	// Update is called once per frame
	void Update () {

        GetInput();
		
	}

    private void GetInput()
    {

        //Testing Health DEBUG ONLY
        if (Input.GetKeyDown(KeyCode.I))
        {
            health.MyCurrentValue -= 10;
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            health.MyCurrentValue += 10;
        }
    }
}
