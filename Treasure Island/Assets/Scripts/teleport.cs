using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class teleport : MonoBehaviour {

    public Transform target;
    static Transform last;
    [SerializeField] bool sizeChange;
    Vector3 playerScale;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (target == last || other.gameObject.tag == "Enemy")
        {
            return;  
        }

        TeleportToExit(other);
    }

    private void OnTriggerExit(Collider other)
    {
        if (target == last)
        {
            last = null;
        }
    }

    void TeleportToExit (Collider other)
    {
        if (sizeChange)
        {
            other.transform.localScale = new Vector3(0.35f, 0.35f, 0.35f);
        }

        else
        {
            other.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
        }

        last = transform;
        other.transform.position = target.transform.position;
    }
}
