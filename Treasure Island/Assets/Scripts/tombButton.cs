using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tombButton : MonoBehaviour {

    public Material activated;
    bool isActive;

	// Use this for initialization
	void Start () {

        isActive = false;
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void changeColour()
    {
        if (isActive == false)
        {
            Renderer rend = GetComponent<Renderer>();
            FindObjectOfType<audioManager>().Play("Button ");
            rend.material = activated;
            isActive = true;
        }
        
    }

    public bool getState()
    {
        return isActive;
    }
}
