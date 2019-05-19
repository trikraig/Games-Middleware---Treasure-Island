using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class boat : MonoBehaviour
{

    public GameObject levelChanger;
    GameObject player;
    Animator anim;
    LevelChanger lC;

    bool missionReady = false;


    // Use this for initialization
    void Start()
    {
        //Accesses singular instance of levelChanger component. 
        lC = levelChanger.GetComponent<LevelChanger>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        //If conditions are met will start the leaving animation, contains event at end to call LoadNextLevel().
        if (missionReady)
        {
            anim.SetTrigger("Leave");
            FindObjectOfType<audioManager>().Play("Boat");
        }

    }

    private void OnTriggerEnter(Collider other)
    {

        //Checks if player has required items to trigger level transition sequence.
        if (other.tag == "Player" && Inventory.instance.HasMapAndCompass())
        {
            missionReady = true;

        }

    }

    private void LoadNextLevel() //Triggered by animation event when reached particular position. 
    {
        //Calls function in singular levelChanger instance.
        FindObjectOfType<audioManager>().Stop("Boat");
        lC.FadeToNextLevel();

    }


}
