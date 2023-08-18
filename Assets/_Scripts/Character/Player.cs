using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    public static Player instance;

    

    [Header("Ink")]
    public float inkDrain = 5f;
    public float inkMax = 100f;
    public float ink;
    public float inkRatio => ink/ inkMax;
    [SerializeField] float iFrameSeconds = 1f;
    [SerializeField] ParticleSystem hurtVFXPrefab;
    [SerializeField] ParticleSystem drainVFX;
    [SerializeField] AudioSource drainSound;




    Rigidbody2D body;
    CharacterActionManager stateManager;
    bool isDraining;



    public void Awake() {
        base.Awake();
        if (instance == null)
            instance = this;
        else {
            Destroy(gameObject);
            return;
        }
        base.Awake();
        ink = inkMax;
        stateManager = GetComponent<CharacterActionManager>();
        body = GetComponent<Rigidbody2D>();
    }

    void Update() {
        if (!isDead && !Game.isLevelComplete)
            DrainInk();
        else StopDraining();
        if (ink < 0) Die();
        if (transform.position.y < -1000f) Die(); // killfloor
    }


    void DrainInk() {
        if (body.velocity.magnitude > 0.1f) {
            StopDraining();
            return;
        }

        if (!isDraining) {
            StartDraining();
        }

        ink -= inkDrain * Time.deltaTime;
    }

    void StartDraining() {
        isDraining = true;
        drainVFX.Play();
        drainSound.Play();
    }

    void StopDraining() {
        isDraining = false;
        drainVFX.Stop();
        drainSound.Stop();
    }




    public static void LoseInk(float amount) {
        if (instance == null) return;
        instance.ink -= amount;
    }

    public static void GainInk(float amount) {
        if (instance == null) return;
        instance.ink = Mathf.Min(instance.ink + amount, instance.inkMax);
    }

    public override void Die() {
        if (isDead) return;
        AddHurtVFX();
        audioPlayer.Play(SoundType.Death);


        base.Die();
        HUD.instance.ShowGameOver();

        FreezeBody();
        GetComponent<Animator>().Play("Die");
    }


    public void FreezeBody() {
        Rigidbody2D body = GetComponent<Rigidbody2D>();
        body.constraints = RigidbodyConstraints2D.FreezeAll;
        GetComponent<Collider2D>().enabled = false;
    }

    public void UnFreezeBody() {
        GetComponent<Collider2D>().enabled = true;
        Rigidbody2D body = GetComponent<Rigidbody2D>();
        body.constraints = RigidbodyConstraints2D.FreezeRotation;
    }


    public override void TakeDamage(float amount) {
        if (isImmune) return;
        if (isDead) return;

        Debug.LogWarning($"player took damage: {amount}");

        ink -= amount;
        if (ink < 0) Die();
        else {
            AddHurtVFX();
            StartImmune();
            stateManager.EnterState(ActionState.Hurt);
            Invoke(nameof(EndHurt), 0.3f);
            CameraShake.Shake();
            audioPlayer.Play(SoundType.Hurt);
        }
    }

    void EndHurt() {
        stateManager.EnterState(ActionState.Idle, forced: true);
    }

    public void StartImmune() {
        isImmune = true;
        Invoke(nameof(EndImmune), iFrameSeconds);
    }

    void EndImmune() {
        isImmune = false;
    }



    void AddHurtVFX() {
        if (!hurtVFXPrefab) return;
        ParticleSystem vfx = Instantiate(hurtVFXPrefab);
        vfx.transform.position = transform.position;
        vfx.Play();
    }


}
