
using UnityEngine;

public class onDamageEnter : MonoBehaviour {

    private void OnTriggerEnter(Collider other) //Deals damage to entity that makes contact with a health component. For use with traps.
    {
        Health otherHealth = other.GetComponent<Health>();
        if (otherHealth)
        {
            otherHealth.TakeDamage(1f);
        }
    }
}
