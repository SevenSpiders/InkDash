using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAction : CharacterAction
{
    public float damage = 10f;
    [SerializeField] List<HitBoxTrigger> hitBoxes;

    void Awake() {
        hitBoxes.ForEach(h => h.o_Hit += HandleHit);
    }

    void Update() {
        if (controller.GetAttackInput()) {
            Attack();
        }
    }

    void Attack() {

    }

    void UnsubScribe() {
        hitBoxes.ForEach(h => h.o_Hit -= HandleHit);
    }


    void HandleHit(IHealthSystem other) {
        other.TakeDamage(damage);
    }
}
