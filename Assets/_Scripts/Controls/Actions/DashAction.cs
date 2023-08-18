using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashAction : CharacterAction
{

    [SerializeField] float dashTime = 0.2f;
    [SerializeField] float dashPower = 24f;
    [SerializeField] float dashCooldown = 0.4f;
    [SerializeField] GameObject hitBox;
    [SerializeField] ParticleSystem vfxPrefab;
    [SerializeField] ParticleSystem dashVFX;
    [SerializeField] float inkCost = 10f;





    
    MoveAction moveAction;

    bool canDash = true;
    // float dashDelay = 0.15f;
    Vector2 savedVelocity = Vector2.zero;
    int playerLayer, enemyLayer;


    void Awake() {
        moveAction = GetComponent<MoveAction>();
        playerLayer = LayerMask.NameToLayer("Player");
        enemyLayer = LayerMask.NameToLayer("Enemy");
        Physics2D.IgnoreLayerCollision(playerLayer, enemyLayer, false);
        hitBox.SetActive(false);
    }

    void Update() {
        if (controller.GetDashInput() && canDash && !isDashing) {
            Dash();
        }
    }


    void Dash() {
        EnterState(ActionState.Dashing);
        canDash = false;
        float faceDirection = manager.isFacingRight ? 1f :-1f;
        savedVelocity = body.velocity;
        body.velocity = new Vector2(faceDirection * dashPower, 0f);
        audioPlayer.Play("Dash");
        PlayVFX();
        Invoke(nameof(EndDash), dashTime);
        Physics2D.IgnoreLayerCollision(playerLayer, enemyLayer, true);
        hitBox.SetActive(true);
        Player.LoseInk(inkCost);
        CameraShake.Shake();
    }

    void PlayVFX() {
        dashVFX.transform.eulerAngles = isFacingRight ? new Vector3(0, 180f, 0) : Vector3.zero;
        dashVFX.Play();
        
    }

    void AddVFX() {
        ParticleSystem vfx = Instantiate(vfxPrefab);
        vfx.transform.position = transform.position;
        vfx?.Play();
    }


    void EndDash() {
        EnterState(ActionState.Running, forced: true);
        Invoke(nameof(ResetCooldown), dashCooldown);
        body.velocity = savedVelocity;
        dashVFX.Stop();
        Physics2D.IgnoreLayerCollision(playerLayer, enemyLayer, false);
        AddVFX();
        hitBox.SetActive(false);
    }

    void ResetCooldown() {
        canDash = true;
    }
}
