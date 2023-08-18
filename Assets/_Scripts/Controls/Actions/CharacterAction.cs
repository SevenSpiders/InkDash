using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAction : MonoBehaviour
{
    protected CharacterActionManager manager;
    protected InputController controller;
    protected Rigidbody2D body;
    protected ContactCheck contactCheck;
    protected AudioPlayer audioPlayer;
    protected Animator animator;

    public bool isFacingRight => manager.isFacingRight;
    public bool isIdle => manager.isIdle;
    public bool isRunning => manager.isRunning;
    public bool isJumping => manager.isJumping;
    public bool isDashing => manager.isDashing;
    public bool isShooting => manager.isShooting;
    public bool isAttacking => manager.isAttacking;
    public bool isClinging => manager.isClinging;
    public bool isCrouching => manager.isCrouching;
    public bool isFalling => manager.isFalling;


    public void Init(
            CharacterActionManager manager,
            InputController controller, 
            Rigidbody2D body, 
            ContactCheck contactCheck, 
            AudioPlayer audioPlayer,
            Animator animator
            ) {
        this.manager = manager;
        this.controller = controller;
        this.body = body;
        this.contactCheck = contactCheck;
        this.audioPlayer = audioPlayer;
        this.animator = animator;
    }

    protected void EnterState(ActionState state, bool forced = false) => manager.EnterState(state, forced);
    // protected void ExitState(ActionState state) => manager.ExitState(state);
    
}
