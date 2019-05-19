using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keyController : MonoBehaviour {

    public Item item;

   //public  GameObject gate;
    
    bool isEmpty;
    bool inRange;
    Input checkInput;
    

    // Use this for initialization
    void Start ()
    {
        isEmpty = false;
        
    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.E) && inRange == true)
        {
            FindObjectOfType<audioManager>().Play("Pickup");
            removeKey();
        }

    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Player") //Needed to prevent enemy ghosting your location. 
        {
            inRange = true;
        }
        



    }

    private void OnTriggerExit(Collider other)
    {
        inRange = false;
    }

    void removeKey()
    {
        if (isEmpty == false)
        {
            Inventory.instance.Add(item);
            DestroyObject(this.gameObject);

        }

        isEmpty = true;
    }
}
