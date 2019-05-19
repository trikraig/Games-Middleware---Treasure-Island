using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : ScriptableObject { //Exists outside the game world and not attahed to a game object.

    public string itemName;
    public Sprite icon;

    public virtual void Use() //Each item can overrite the virtual function via inheritance.
    {

    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
