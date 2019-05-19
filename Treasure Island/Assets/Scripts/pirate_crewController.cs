using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class pirate_crewController : MonoBehaviour {

    NavMeshAgent agent;
    [SerializeField] GameObject player;
    public float damageOutput = 1f;
    Animator anim;
    Vector3 startPos;
    public bool isBoss;
    public bool patrol;
    bool playerInRange;
    string state;

    //Navigation
    public Transform[] points;
    private int destPoint = 0;


    // Use this for initialization
    void Start () {

        agent = GetComponent<NavMeshAgent>();
        startPos = transform.position;
        anim = GetComponent<Animator>();
        state = "Movement";
        player = GameObject.FindGameObjectWithTag("Player");
        
        //Navigation
        agent.autoBraking = false;
        GotoNextPoint();
        
	}

    // Update is called once per frame
    void Update()
    {

        switch (state)
        {
            case "Movement":
                {
                    Move();
                }
                break;

            case "Attack":
                {

                }
                break;

        }
    }

    void GotoNextPoint()
    {
        // Returns if no points have been set up
        if (patrol == false)
        {
            return;
        }
            

        // Set the agent to go to the currently selected destination.
        agent.destination = points[destPoint].position;

        // Choose the next point in the array as the destination,
        // cycling to the start if necessary.
        destPoint = (destPoint + 1) % points.Length;
    }

    void PlaySound(string name)
    {
        FindObjectOfType<audioManager>().Play(name);
    }

    void DealWeaponDamage()
    {
        //Calculate center of hitbox
        Vector3 center = transform.position + (transform.up) + (transform.forward); //Hit box origin in relation to player's position.
        Vector3 halfExtents = new Vector3(5.5f, 6f, 0.5f); //Extend the box to full size.
        Collider[] hits = Physics.OverlapBox(center, halfExtents, transform.rotation); //Array of every collider touched by the OverlapBox at postion center with size "halfExtends".
        
        //If player is located inside hitbox, damage is applied unless blocked. Otherwise no damage and block sound player. 
        foreach (Collider hit in hits)
        {
            if (hit.tag == "Player")
            {
                Health otherHealth = hit.GetComponent<Health>();
                if (otherHealth && hit.GetComponent<playerController>().GetIsBlocking() == false)
                {
                    PlaySound("Sword");
                    otherHealth.TakeDamage(damageOutput);
                }
                else
                {
                    PlaySound("Block");
                }
            }
        }
    }

    void Move()
    {
        //Check if lost player location.
        if (player.transform.position == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }

        Vector3 target = player.transform.position;

        //Stopping distance from target.
        agent.stoppingDistance = 4f;

        //If player is outside min distance, returns to start position, or goes back to patrolling between waypoints. 
        //Otherwise sets the player as the target destination as soon as comes into range.
        if (Vector3.Distance(transform.position, target) > 7)
        {
            target = startPos;
            playerInRange = false;

            if (patrol == true || isBoss == false)
            {
                GotoNextPoint();

            }
            else
            {
                agent.SetDestination(target);
            }
        }

        else
        {
            playerInRange = true;
            agent.SetDestination(target);
        }

         
        //Action when player is in range.
        if (playerInRange)
        {
            //2% chance to attack.
            if (Random.Range(0, 100f) < 2f) 

            {
                if (isBoss)
                {
                    PlaySound("BossRoar");
                }
                else
                {
                    PlaySound("Arrr");
                }

                ChangeState("Attack");
            }
        }
          
        //Controls enemy movement animation depending on speed of movement.                 
        anim.SetFloat("movePercent", agent.velocity.magnitude / agent.speed);
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

    void onDeath()
    {
        if (isBoss)
        {
            PlaySound("BossDeath");
        }
        else
        {
            PlaySound("PlayerDeath");
        }
        
        anim.SetTrigger("Death");
        //Time to play hurt animation before deleting itself.
        //Maybe add Destoy to event called at end of animation?
        Destroy(gameObject, 2f);
    }
}
