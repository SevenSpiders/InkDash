using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBoxTrigger : MonoBehaviour
{

    public System.Action<IHealthSystem> o_Hit;

    [SerializeField] string tagToHit = "Player"; // "Enemy"
    [SerializeField] float damage = 10f;

    private void OnTriggerEnter2D(Collider2D other) {

        // IHealthSystem healthSystem = other.GetComponent<IHealthSystem>();
        // if (healthSystem != null)
        //    o_Hit?.Invoke(healthSystem);
        
        if (other.TryGetComponent<IHealthSystem>(out IHealthSystem healthSystem)) {
            if (!other.CompareTag(tagToHit)) return;
            o_Hit?.Invoke(healthSystem);
            healthSystem.TakeDamage(damage);
        }
        
    }
}
