using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootRay : MonoBehaviour {

    protected Animator anim;

    RaycastHit hit;

    Vector3 hitPoint;

   
   private GameObject target;
    

   public int enemyDamage = 1;

    

   
    

    private void Awake()
    {
        
    }


    // Use this for initialization
    void Start () {

        anim = GetComponentInParent<Animator>();
		
	}
	
	// Update is called once per frame
	void Update () {

        Ray ray = new Ray(transform.position, transform.forward);

        if (Physics.Raycast(ray,out hit))

        if (hit.collider != null && Input.GetMouseButtonDown(0)) //If hit something.
            {
                
                anim.SetBool("Fire", true);
                



                Debug.DrawRay(ray.origin, ray.direction * hit.distance, Color.yellow);
                target = hit.transform.gameObject;
                hitPoint = hit.point;
                Debug.Log("x " + hitPoint.x + " y " + hitPoint.y + " z " + hitPoint.z);
                Debug.Log("PEW PEW!!" + "Tag: " + target.tag);

            if (target.tag == "Enemy")
                {
                    enemy targetEnemy = target.GetComponent<enemy>();
                    targetEnemy.TakeDamage(enemyDamage);


                }
            }

            else
            {
                target = null;
                //anim.SetBool("Fire", false);
            }

       
		
	}

   


}
