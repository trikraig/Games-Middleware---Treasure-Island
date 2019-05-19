using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trapTrigger : MonoBehaviour {

    public GameObject trap;
    Animation anim;
    bool hasntSprung;

    // Use this for initialization
    void Start () {

        anim = trap.GetComponent<Animation>();
        hasntSprung = false;
	}
	
    private void OnTriggerEnter(Collider other)
    {
        //Checks if trap has activated once, plays animation attached to trap if not. 
        if (hasntSprung == false) 
        {
            Debug.Log("Spring Trap");
            anim.Play("roller");
            hasntSprung = true;
        }
        
    }
}
