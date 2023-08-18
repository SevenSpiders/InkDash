using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CharacterActionManager : MonoBehaviour
{

    public System.Action<bool> o_Dash;
    public System.Action o_Run;


    [SerializeField] GameObject actionsContainer;
    [SerializeField] SpriteRenderer spriteRenderer;
    CharacterAction[] actions;

    public ActionState state {get; private set;}
    public ActionState previousState {get; private set;}
    public bool isFacingRight {get; private set;}

    InputController controller;
    Rigidbody2D body;
    ContactCheck contactCheck;
    AudioPlayer audioPlayer;
    Animator animator;


    public bool isIdle => state == ActionState.Idle;
    public bool isRunning => state == ActionState.Running || isShootingRunning;
    public bool isJumping => state == ActionState.Jumping;
    public bool isFalling => state == ActionState.Falling;
    public bool isDashing => state == ActionState.Dashing;
    public bool isShooting => state == ActionState.Shooting || isShootingRunning;
    public bool isAttacking => state == ActionState.Attacking;
    public bool isClinging => state == ActionState.Clinging;
    public bool isCrouching => state == ActionState.Crouching;
    public bool isShootingRunning => state == ActionState.ShootingRunning;

    

    void Awake() {

        actions = actionsContainer.GetComponents<CharacterAction>();
        isFacingRight = true;


        controller = GetComponent<InputController>();
        body = GetComponent<Rigidbody2D>();
        contactCheck = GetComponent<ContactCheck>();
        audioPlayer = GetComponent<AudioPlayer>();
        animator = GetComponent<Animator>();

        foreach (CharacterAction action in actions) {
            action.Init(
                manager: this,
                controller: controller,
                body: body,
                contactCheck: contactCheck,
                audioPlayer: audioPlayer,
                animator: animator
            );
        }
    }

    public void Flip() {
        isFacingRight = !isFacingRight;
        spriteRenderer.flipX = !isFacingRight;
    }


    public void EnterState(ActionState s, bool forced = false) {

        // print($"state: {state} => {s}");

        // Locked States
        if (!forced) {
            // if (state == ActionState.Falling) return;
            if (state == ActionState.Dashing) return;
            if (state == ActionState.Clinging) return;
            if (state == ActionState.Hurt) return; 
        }
        

        ExitState(state);

        previousState = state;
        state = s;

        switch (s)
        {
            case ActionState.Idle:
                animator.Play("Idle");
                break;
            
            case ActionState.Running:
                animator.Play("Run");
                break;
            
            case ActionState.ShootingRunning:
                animator.Play("ShootRun");
                break;
            
            case ActionState.Crouching:
                animator.Play("Crouch");
                break;
            
            case ActionState.Jumping:
                animator.Play("Jump");
                break;
            
            case ActionState.Falling:
                animator.Play("Fall");
                break;
            
            case ActionState.Landing:
                animator.Play("Land");
                break;
            
            case ActionState.Hurt:
                animator.Play("Hurt");
                break;
            
            case ActionState.Dead:
                animator.Play("Dead");
                break;
            
            case ActionState.Dashing:
                o_Dash?.Invoke(true);
                animator.Play("Dash");
                break;
            
            case ActionState.Clinging:
                float wall = contactCheck.GetWallDirection();
                if (wall > 0 != isFacingRight) Flip();
                animator.Play("Cling");
                break;
            
            default:
                Debug.LogWarning($"State has no entry definition: {s}");
                break;
        }
    }

    void ExitState(ActionState s) {
        switch (s)
        {
            case ActionState.Idle:
                break;
            
            case ActionState.Running:
                break;
            
            case ActionState.ShootingRunning:
                break;
            
            case ActionState.Crouching:
                break;
            
            case ActionState.Jumping:
                break;
            
            case ActionState.Dashing:
                o_Dash?.Invoke(false);
                break;
            
            case ActionState.Clinging:
                break;
            
            default:
                break;
        }
        
    }
}
