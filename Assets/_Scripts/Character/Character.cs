using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[SelectionBase]
public class Character : MonoBehaviour, IHealthSystem
{

    public static System.Action<Character> o_Death;


    protected AudioPlayer audioPlayer;

    public float healthMax = 100f;
    public float health {get; private set;}
    public bool isDead;
    public bool isImmune {get; protected set;}




    public void Awake() {
        health = healthMax;
        audioPlayer = GetComponent<AudioPlayer>();
    }


    public virtual void TakeDamage(float amount) {
        health -= amount;
        if (health <= 0) Die();
        else audioPlayer?.Play("Hurt");
        print($"{name} took {(int) amount} damage");
    }

    public virtual void Heal(float amount) {
        health = Mathf.Min(health + amount, healthMax);
    }


    public virtual void Die() {
        isDead = true;
        o_Death?.Invoke(this);
        audioPlayer?.Play("Death");
    }
    
}
