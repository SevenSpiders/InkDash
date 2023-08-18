using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    [SerializeField] ParticleSystem hurtVFXPrefab;

    public override void TakeDamage(float amount) {
        base.TakeDamage(amount);
        if (isDead) AddHurtVFX();
    }



    void AddHurtVFX() {
        if (!hurtVFXPrefab) return;
        ParticleSystem vfx = Instantiate(hurtVFXPrefab);
        vfx.transform.position = transform.position;
        vfx.Play();
    }

    public override void Die() {
        AddHurtVFX();
        Destroy(gameObject);
    }
}
