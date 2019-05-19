using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponRackController : MonoBehaviour

{

    public Item item;
    bool isEmpty;
    bool inRange;
    public GameObject sword;
    Input userInput;

    // Use this for initialization
    void Start()
    {
           
        isEmpty = false;
    }

    void Update()
    {
       if (Input.GetKeyDown(KeyCode.E) && inRange == true)
        {
            FindObjectOfType<audioManager>().Play("Pickup");
            removeSword();
        }
    }



    private void OnTriggerEnter(Collider other)
    {
        inRange = true;
    


    }

    private void OnTriggerExit(Collider other)
    {
        inRange = false;
    }

    void removeSword()
    {
        if (isEmpty == false)
        {
            Inventory.instance.Add(item);
            DestroyObject(sword);

        }
        
        isEmpty = true;

    }
}
