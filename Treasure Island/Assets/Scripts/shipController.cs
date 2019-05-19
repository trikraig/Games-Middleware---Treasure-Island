using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shipController : MonoBehaviour {

    private Transform seaPlane;
    private Cloth planeCloth;
    [SerializeField] private int closestVertexIndex = -1;
    

	// Use this for initialization
	void Start () {

        seaPlane = GameObject.Find("Water Plane").transform;
        planeCloth = seaPlane.GetComponent<Cloth>();
		
	}
	
	// Update is called once per frame
	void Update () {

        GetClosestVertex();
		
	}

    void GetClosestVertex()
    {
        //Compares closest vertex and current vertex.
        for (int i = 0; i < planeCloth.vertices.Length; i++)
        {
            if(closestVertexIndex == -1) //If havent assigned yet.
            {
                closestVertexIndex = i;
            }

            float distance = Vector3.Distance(planeCloth.vertices[i], transform.position);
            float closestDistance = Vector3.Distance(planeCloth.vertices[closestVertexIndex], transform.position);

            if (distance  < closestDistance)
            {
                closestVertexIndex = i;
            }
        }
        //Update height of ship.
        transform.localPosition = new Vector3(
            transform.localPosition.x,
            planeCloth.vertices[closestVertexIndex].y/10,
            transform.localPosition.z);
    }
}
