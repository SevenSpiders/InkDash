using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBoxCollider : MonoBehaviour
{

    public System.Action<IHealthSystem> o_Hit;

    [SerializeField] string tagToHit = "Player"; // "Enemy"
    [SerializeField] float damage = 10f;

    GameObject other;

    private void OnCollisionEnter2D(Collision2D collision) {
        Hurt(collision);
    }

    void OnCollisionStay2D(Collision2D collision) => Hurt(collision);



    void Hurt(Collision2D collision) {
        other = collision.gameObject;
        if (other.TryGetComponent<IHealthSystem>(out IHealthSystem healthSystem)) {
            if (!other.CompareTag(tagToHit)) return;
            o_Hit?.Invoke(healthSystem);
            healthSystem.TakeDamage(damage);
        }
    }
}
