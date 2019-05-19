using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerScript : MonoBehaviour {

    bool gateOpen;

    public bool unlocked = false;
    
    Animation anim;

    

    private void Start()
    {
        
        gateOpen = false;
        anim = GetComponent<Animation>();
        
        

    }

    private void OnTriggerEnter(Collider other)
    {
        //GameObject player = Inventory.instance.player;//Access the player component via the inventory instance.
        

        Debug.Log("Entered Zone");

        if (Inventory.instance.HasKey())
        {
            unlocked = true;
            Debug.Log("Unlocked");
        }
        


        if(unlocked)
        {
            if (other.tag == "Player" && gateOpen == false)

            {
                FindObjectOfType<audioManager>().Play("Gate");
                Debug.Log("Playing Animation");
                anim.Play("door open");
                gateOpen = true;

            }
        }

        
    }

   
}
