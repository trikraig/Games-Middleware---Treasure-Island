using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerController : MonoBehaviour
{

    CharacterController cc; //Creates reference to character controller
    Animator anim;//Creates reference to the Animator of the main character.
    Camera cam;
    Health health;
    public GameObject sword;
    public GameObject gun;

    public GameObject bullet;
    public Transform shootOrigin;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;
    public GameObject ip;

    int maxBullets = 100;
    int bullets;

    public Text ammoText;

    Cinemachine.CinemachineBrain cinematic;
    public GameObject mainCamera;
    bool hasDisabled = false;

    public float damage = 1f;
    public float range = 10f;

    RaycastHit hit;
    Vector3 hitPoint;
    GameObject target;

    


    bool hasSword = false;
    bool hasGun = false;
    public bool isBlocking;

    bool getHasGun()
    {
        return hasGun;
    }

    bool getHasSword()
    {
        return hasSword;
    }

    public float moveSpeed = 4f; //Public to allow change from inspector.
    float gravity = 0f;//Default gravity force acting upon player.
    float jumpVelocity = 0; //Allows calculation length of jump.
    float jumpHeight = 16f; //Max jump height, public for testing in inspector.

    string state = "Movement";
    public Vector3 checkpoint;



    // Use this for initialization
    void Start()
    {


        Reload();
        cc = GetComponent<CharacterController>(); //Assigns value to reference.
        anim = GetComponent<Animator>();
        health = GetComponent<Health>();
        cam = Camera.main;
        checkpoint = transform.position; //Checkpoint system to record starting position in level.
        
        
        if (mainCamera != null)
        {
            cinematic = mainCamera.GetComponent<Cinemachine.CinemachineBrain>();
        }


    }





    // Update is called once per frame
    void Update()
    {

        if (cinematic != null)
        {
            if (Input.GetMouseButtonDown(0) && hasDisabled == false)
            {
                cinematic.enabled = !cinematic.enabled;
                if (ip != null)
                {
                    ToggleUI();
                    
                }
                
                hasDisabled = true;
            }
        }

        if (ammoText == null)
        {
            ammoText = GameObject.FindWithTag("ammoText").GetComponent<Text>();
            
        }

        if (ammoText != null)
        {
            
            ammoText.text = bullets.ToString();
        }

        

        switch (state) //Impromptu state machine.
        {
            case "Movement":
                Movement();

                SwingSword();
                Block();
                Fire();
                break;

            case "Jump":
                Jump();
                Movement();
                if (state == "Jump" && cc.isGrounded)
                {
                    ChangeState("Movement");
                }
                break;

            default:
                break;
        }

        //Menu(); //Outside of state machine.
    }

    public void equipSword()
    {
        if (hasSword == false)
        {
            gun.SetActive(false);


            sword.SetActive(true);
            hasSword = true;

        }

        else
        {
            sword.SetActive(false);
            hasSword = false;

        }

    }

    public void equipGun()
    {
        if (hasGun == false)
        {
            sword.SetActive(false);
            gun.SetActive(true);

            hasGun = true;

        }

        else
        {
            gun.SetActive(false);

            hasGun = false;

        }
    }

    void DealWeaponDamage()
    {
        //Calculate center of hitbox
        Vector3 center = transform.position + (transform.up) + (transform.forward); //Hit box origin in relation to player's position.
        Vector3 halfExtents = new Vector3(5.5f, 6f, 0.5f); //Extend the box to full size.
        Collider[] hits = Physics.OverlapBox(center, halfExtents, transform.rotation); //Array of every collider touched by the OverlapBox at postion center with size "halfExtends".

        foreach (Collider hit in hits)
        {
            if (hit.tag != "Player")
            {
                Health otherHealth = hit.GetComponent<Health>();
                if (otherHealth)
                {
                    otherHealth.TakeDamage(1f);
                }
            }
        }
    }

    void FireGun(int damageMultiply)
    {
        Debug.Log("Firing Gun");

        //muzzleFlash.Play(); Cannot get to work when transitioning scenes.

        RaycastHit hit;

        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);


            if (hit.transform.tag == "Enemy")
            {
                Health otherHealth = hit.transform.GetComponent<Health>();
                if (otherHealth)
                {
                    PlaySound("Grunt");
                    otherHealth.TakeDamage(0.1f * damageMultiply); //Change to increase damage. Set for testing.
                }
            }

            else if (hit.transform.tag == "Marker") //Tomb Door Puzzle.
            {

                tombButton button = hit.transform.GetComponent<tombButton>();
                button.changeColour();
            }

            Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            bullets -= damageMultiply;

        }


        //GameObject b = Instantiate(bullet, shootOrigin.position, Quaternion.identity);

        //Rigidbody bulletBody = b.GetComponent<Rigidbody>();
        //bulletBody.AddForce(transform.forward * 500f);

        //Add ammo.

        //Ray ray = new Ray(transform.position, transform.forward);


    }

    void FireAltGun()
    {

    }

    //Raycast + damage. 
    //reduce ammo.


    void onDeath()
    {
        PlaySound("PlayerDeath");

        anim.SetTrigger("Death");
    }

    void PlaySound(string name)
    {
        FindObjectOfType<audioManager>().Play(name);
    }

    public bool getSword()
    {
        return hasSword;
    }

    public bool getGun()
    {
        return hasGun;
    }

    void Movement() //Handles walking, running and jumping.
    {


        //Records W (up/forward on keyboard) as 1, S as -1 (down/backwards on keyboard)
        float x = Input.GetAxisRaw("Horizontal");
        //Records D (right on kb) as 1, A (left on kb) as -1.
        float z = Input.GetAxisRaw("Vertical");
        //ensures player moves at same speed in every direction.
        Vector3 direction = new Vector3(x, 0, z).normalized;
        //Stores yaw angle of the main camera.
        float cameraDirection = cam.transform.localEulerAngles.y;
        //Rotating the players direction vector to the yaw angle of the main camera. 
        direction = Quaternion.AngleAxis(cameraDirection, Vector3.up) * direction;
        //stores normalized direction * how far to move * per second
        Vector3 velocity = direction * moveSpeed * Time.deltaTime;
        //Ratio of the current velocity to the players full movement speed.
        float percentSpeed = velocity.magnitude / (moveSpeed * Time.deltaTime);
        //Adjusts float tree inside of animation controller.
        anim.SetFloat("movePercent", percentSpeed);

        //Inbuilt function checks if cc is on ground. case true.            
        if (cc.isGrounded) 
        {
            gravity = 0;
        }
        //case false, player is not on the ground,
        else
        {
            gravity += 0.25f;
            //Ensures player doesnt fall too fast, limits fall speed.
            gravity = Mathf.Clamp(gravity, 1f, 20f); 
        }
        //Checks jump key and if player is on ground.
        if (Input.GetKeyDown(KeyCode.Space) && cc.isGrounded) 
        {
            PlaySound("Jump");
            jumpVelocity = jumpHeight;
            ChangeState("Jump");

        }

        //Downward vector, length is gravity. Limited by time.
        Vector3 gravityVector = -Vector3.up * gravity * Time.deltaTime;
        //Creates jump vector, length is jumpVelocity , direction is up, Limited by time.
        Vector3 jumpVector = Vector3.up * jumpVelocity * Time.deltaTime;
        //Moves the character along the passed vector, applies the gravity and jumping vectors.                   
        cc.Move(velocity + gravityVector + jumpVector);


        //Will only change direction if player is moving. Magnitude represents the size generally. 
        if (velocity.magnitude > 0) 
        {
            // Calculates angle to ensure player moves in direction they are facing. Converted from radians to degrees.
            float yAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;

            // transform.Rotate(new Vector3(0, yAngle, 0));

            //Rotates the player character via transform to the calculated angle.
            transform.localEulerAngles = new Vector3(0, yAngle, 0); 
        }
    }

    void Jump()
    {
        //Decrease velocity until reache zero else return.
        if (jumpVelocity < 0) 
        {
            return;
        }

        jumpVelocity -= 1.25f;
    }

    void ChangeState(string stateName)
    {
        state = stateName;
        anim.SetTrigger(stateName);
    }

    void ReturnToMovement()
    {
        ChangeState("Movement");
    }

    //Restarts level with full health and sets players transform back to starting location.
    void ReturnToCheckpoint() 
    {

        health.Reset();
        ReturnToMovement();
        transform.position = checkpoint;
    }

    void SwingSword() 
    {
        if (Input.GetMouseButtonDown(0) && hasSword == true)
        {

            PlaySound("Sword");
            ChangeState("Swing");
        }

    }

    void Block() 
    {
        if (Input.GetMouseButtonDown(1) && hasSword == true)
        {
            anim.SetTrigger("Block");
            isBlocking = true;
        }
        else
        {
            isBlocking = false;
        }

    }

    public bool GetIsBlocking()
    {
        return isBlocking;
    }

    void Fire()
    {
        if (Input.GetMouseButtonDown(0) && hasGun == true && bullets >= 1)
        {
            PlaySound("Gunshot");
            FireGun(1);
        }

        else if (Input.GetMouseButtonDown(1) && hasGun == true && bullets >= 10)
        {
            PlaySound("Gunshot");
            FireGun(10);
        }
    }





    public void Reload()
    {
        bullets = maxBullets;
    }



    void ToggleUI()
    {


        if (!ip.activeSelf)
        {
            ip.SetActive(true);
        }
        else
        {
            ip.SetActive(false);
        }
        
    }


}
