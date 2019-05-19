using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chestController : MonoBehaviour {

    public Item item;
    bool isEmpty;
    bool inRange;
    Animator anim;
    Input checkInput;

    // Use this for initialization
    void Start () {

        anim = GetComponent<Animator>();
        isEmpty = false;
	}

    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.E)) && inRange == true)
        {
            OpenChest();  
        }
    }
       
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            inRange = true;
        }


    }

    void OpenChest()
    {
        if (isEmpty == false/* && anim != null*/)
        {
            FindObjectOfType<audioManager>().Play("Chest");
            anim.SetTrigger("open");
            AddItem();
        }

        isEmpty = true;
    }

    void AddItem()
    {
        Inventory.instance.Add(item);
    }
}
