using UnityEngine;

public class EnemyHitbox : MonoBehaviour
{
    public float dmgAmount;

    private void OnTriggerEnter(Collider other)
    {
        IDamageable damageable = other.GetComponent<IDamageable>();
        if (damageable != null && other.CompareTag("PLayer"))
        {
            Debug.Log("Player");
            damageable.Damage(dmgAmount);
        }
    }
}
