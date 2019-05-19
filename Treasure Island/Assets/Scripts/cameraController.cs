//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class cameraController : MonoBehaviour {

    GameObject target; //Camera target to follow. - To become player object.
    playerController player;

    public GameObject crosshair;
    public GameObject uiGun;


    float desiredDistance = 4f; //Make 0 for first person. Camera distance from player object 

    //Mouse to control pitch and yaw.
    float pitch = 0f;
    float pitchMin = -40f;
    float pitchMax = 60f;
    float yaw = 0f;
    float roll = 0f;

    //Mouse sensitivity.
    float sensitivity = 10f;

    string state = "3rd Person";

    private void Start()
    {
        //Locate player in scene and reference playerController script.
        findTarget();
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            findTarget();
        }

        //Camera postion state machine.
        switch (state)
        {
            //3rd person sets camera desired distance from the player, disables any first person UI exclusive elements.
            case "3rd Person":
                crosshair.SetActive(false);
                updateGun(false);
                desiredDistance = 5f;
                //desiredDistance = (float)(target.transform.localScale.x * 15);
                break;
           
            //1st person sets camera inside the player object, some tweaking required,  enables first person UI exclusive elements.
            case "1st Person":
                crosshair.SetActive(true);
                updateGun(true);
                desiredDistance = -1f;
                break;
        }

        //Uses mouse position to set pitch and yaw for camera, causes camera to respond to mouse movement.
        pitch -= sensitivity * Input.GetAxis("Mouse Y");
        yaw += sensitivity * Input.GetAxis("Mouse X");

        //Clamps the maximum pitch between min and max. some tweaking required to ensure does not clip levels.
        pitch = Mathf.Clamp(pitch, pitchMin, pitchMax);

        //Transforms position of the camera relative to the inputted values. 
        transform.localEulerAngles = new Vector3(pitch, yaw, roll);
        transform.position = target.transform.position - desiredDistance * transform.forward + Vector3.up * 1.5f;
        //Set camera position to the players position MINUS the forward direction. 
        //Increase height by Vector3.up

        if (Input.GetKeyDown(KeyCode.C)) //Toggle 1st/3rd person camera.
        {
            if (state == "3rd Person")
            {
                ChangeState("1st Person");
            }

            else
            {
                ChangeState("3rd Person");
            }
        }


    }
    //Locates player object and controller script.
    public  void findTarget()
    {
        target = GameObject.FindWithTag("Player");
        player = target.GetComponent<playerController>();
    }

    //Toggles the UI gun object in first person view.
    void updateGun(bool enable)
    {
        if(player.getGun())
        {
            if (enable)
            {
                uiGun.SetActive(true);
            }

            else
            {
                uiGun.SetActive(false);
            }
        }

    }

    
    //Changes player state to passed in string name.
    void ChangeState(string stateName)
    {
        state = stateName;
    }
}
