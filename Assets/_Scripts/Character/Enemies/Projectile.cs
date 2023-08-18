using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float damage = 10f;
    public float speed = 20f;
    [SerializeField] ParticleSystem impactVFX;
    
    Rigidbody2D body;


    void Start() {
        body = GetComponent<Rigidbody2D>();
        body.velocity = transform.right * speed;
    }

    void OnTriggerEnter2D(Collider2D info) {
        IHealthSystem target = info.gameObject.GetComponent<IHealthSystem>();
        if (target != null) {
            target.TakeDamage(damage);
            
        }
        Impact();
        Invoke(nameof(SelfDestruct), 0.2f);
    }

    void SelfDestruct() {
        Destroy(gameObject);
    }

    void Impact() {
        if (impactVFX != null) {
            ParticleSystem obj = Instantiate(impactVFX);
            obj.transform.position = transform.position;
            obj.Play();
        }
    }

}
