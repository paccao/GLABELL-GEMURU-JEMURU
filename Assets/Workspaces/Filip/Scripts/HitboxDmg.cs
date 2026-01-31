using Unity.VisualScripting;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HitboxDmg : MonoBehaviour
{
    public float dmgAmount;

    private void OnTriggerEnter(Collider other)
    {
        IDamageable damageable = other.GetComponent<IDamageable>();
        if (damageable != null && other.CompareTag("Enemy"))
        {
            Debug.Log("Fisk in");
            damageable.Damage(dmgAmount);
        }
    }
}
