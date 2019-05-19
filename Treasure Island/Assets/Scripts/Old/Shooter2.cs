using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Shooter2 : MonoBehaviour
{
    public Rigidbody bullet;
    public int power = 1500;
    void Start()
    {
    }
    void Update()
    {
        if (Input.GetKeyDown("f"))
        {
            Rigidbody newInstance = Instantiate(bullet, transform.position, transform.rotation) as Rigidbody;
            Vector3 fwd = transform.TransformDirection(Vector3.forward);
            newInstance.AddForce(fwd * power, ForceMode.Force);
        }
    }
}
