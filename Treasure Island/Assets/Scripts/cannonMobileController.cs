using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class cannonMobileController : MonoBehaviour {

    NavMeshAgent nav;
    public GameObject player;
    Animator anim;
    Vector3 anchor;
    public GameObject ball;
    public Transform shootOrigin;

    string state;

    // Use this for initialization
    void Start()
    {
                nav = GetComponent<NavMeshAgent>();
        anchor = transform.position;
        anim = GetComponent<Animator>();
        state = "Movement";
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

    void Move()
    {
        Vector3 target = player.transform.position;
        nav.stoppingDistance = 4f;

        if (Vector3.Distance(transform.position, target) > 7)
        {
            target = anchor; //If target is move than 7 units away will switch navigation target to anchor.
            nav.stoppingDistance = 0f;
        }
        else
        {
            if (Random.Range(0, 100f) < 2f) //2% chance to attack.
            {
                ChangeState("Attack");
            }
        }

        nav.SetDestination(target);
        //anim.SetFloat("movePercent", nav.velocity.magnitude / nav.speed); - No moving animation yet made for cannon.
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

    void ShootBall()
    {
        //Instatiates cannon ball object, applies forward motion. Destruction is controlled in projectile script. 
        GameObject b = Instantiate(ball, shootOrigin.position, Quaternion.identity);

        Rigidbody ballBody = b.GetComponent<Rigidbody>();
        ballBody.AddForce(transform.forward * 500f);
    }
}
