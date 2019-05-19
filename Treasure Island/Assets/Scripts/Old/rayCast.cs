using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rayCast : MonoBehaviour {


   void Start()
    {
        
    }

    void Update()
    {

        Ray ray = new Ray(transform.position, transform.forward);
        Debug.Log("Ray created");
        Vector3 hitPoint;

        RaycastHit hit;
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray, out hit, 100)) //Checks to see if ray hits object
                if (hit.collider != null)
                {
                    Debug.DrawRay(ray.origin, ray.direction * 10, Color.yellow); //Makes ray visible ONLY IN SCENE VIEW
                    hitPoint = hit.point;

                    Debug.Log(" x " + hitPoint.x + " y " + hitPoint.y + " z " + hitPoint.z);
                    StartCoroutine("Fade");
                }
            Debug.DrawLine(ray.origin, hit.point); //Drws line from origin to intersection point.
        }
    }

    IEnumerator Fade()
    {
        for (float f = 1f; f >= 0; f -= 0.01f)
        {
            Color c = GetComponent<Renderer>().material.color;
            c.a = f;
            GetComponent<Renderer>().material.color = c;
            if (f <= 0.1)
            {
                Destroy(gameObject);
            }
            yield return new WaitForSeconds(.1f);
        }
    }
}
