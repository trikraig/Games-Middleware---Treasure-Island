using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tombDoor : MonoBehaviour {

    public tombButton buttonA;
    public tombButton buttonB;
    public tombButton buttonC;
    bool hasPlayed = false;
    Animation anim;

	// Use this for initialization
	void Start () {

        anim = GetComponent<Animation>();
		
	}

    // Update is called once per frame
    private void FixedUpdate()
    {
        CheckDoor();
    }

    void CheckDoor()
    {
        if (buttonA.getState() && buttonB.getState() && buttonC.getState() && hasPlayed == false)
        {
            FindObjectOfType<audioManager>().Play("Tombdoor");
            FindObjectOfType<audioManager>().Stop("TombBG");
            anim.Play("tombDoor");
            hasPlayed = true;
        }
    }
}
