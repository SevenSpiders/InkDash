using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Collider2D))]
public class KillZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {

        
        if (other.TryGetComponent<IHealthSystem>(out IHealthSystem healthSystem)) {
            healthSystem.Die();
        }
        
    }
}
