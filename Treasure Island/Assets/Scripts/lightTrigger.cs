using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightTrigger : MonoBehaviour {

    
    bool hasPlayed;
    Animation sunAnim;
    public Material otherSkybox;

	// Use this for initialization
	void Start () {
        sunAnim = GetComponent<Animation>();
        hasPlayed = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && hasPlayed == false)
        {
            sunAnim.Play("sun");

            hasPlayed = true;
        }
    }

   void changeSkybox()
    {
        RenderSettings.skybox = otherSkybox;
    }
}
