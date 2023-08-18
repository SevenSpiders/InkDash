using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallActions : CharacterAction
{
    [SerializeField] float wallStickTime;
    [Header("Slide")]
    [SerializeField] float maxWallSlideSpeed = 3f;

    [Header("Wall Jump")]
    [SerializeField] Vector2 wallJumpClimb = new Vector2(4f, 12f);
    [SerializeField] Vector2 wallJumpBounce = new Vector2(10f, 10f);
    [SerializeField] Vector2 wallJumpLeap = new Vector2(14f, 12f);


    MoveAction moveAction;
    Vector2 velocity;
    bool wallContact;
    bool groundContact;
    bool shouldJump;
    bool isUpOnWall => wallContact && !groundContact;
    float wallDirection;
    float airTime;
    float stickTimer;

    void Awake() {
        moveAction = GetComponent<MoveAction>();
    }

    void Update() {

        // should be clinging?
        if (isUpOnWall) {

            if (!isClinging) {
                EnterState(ActionState.Clinging);
            }

            
        }

        if (isClinging) {
            if (!isUpOnWall)
                EnterState(ActionState.Idle, forced: true);
            
            else shouldJump = controller.GetJumpInput();
        }
    }


    void FixedUpdate() {
        velocity = body.velocity;
        groundContact = contactCheck.groundContact;
        wallContact = contactCheck.wallContact;
        wallDirection = contactCheck.GetWallDirection();

        if (isJumping || isFalling) airTime += Time.deltaTime;
        // if (isJumping && groundContact) Reset();
        // if (isJumping && wallContact && airTime > 0.1f) Reset();

        // Wall Stick
        // if (isUpOnWall && !isJumping) StickToWall();
        // else UnstickToWall();

        // Handle Updates
        HandleWallSlide();
        HandleWallJump();

        body.velocity = velocity;
    }

    void Reset() {
        airTime = 0;
        velocity = Vector2.zero;
    }

    void StickToWall() {
        
        if ( isMovingIntoWall() ) {
            stickTimer = wallStickTime;
            moveAction.StickToWall(false);
        } else {
            stickTimer -= Time.deltaTime;
            moveAction.StickToWall(stickTimer > 0);
        }
    }

    void UnstickToWall() => moveAction.StickToWall(false);


    void HandleWallSlide() {
        if (isUpOnWall) {
            velocity.y = Mathf.Max(velocity.y, - maxWallSlideSpeed);
        }
    }

    void HandleWallJump() {
        if (!shouldJump) return;

        // Wall Bounce
        if (controller.GetMoveInput() == 0) {
            velocity = new Vector2( wallJumpBounce.x * wallDirection, wallJumpBounce.y);
            EnterState(ActionState.Jumping, forced: isClinging);
            shouldJump = false;
        }

        // Wall Climb
        else if ( isMovingIntoWall() ) {
            velocity = new Vector2( wallJumpClimb.x * wallDirection, wallJumpClimb.y);
            EnterState(ActionState.Jumping, forced: isClinging);
            shouldJump = false;
        }

        // Wall Leap
        else if ( isMovingOffWall() ) {
            velocity = new Vector2( wallJumpLeap.x * wallDirection, wallJumpLeap.y);
            EnterState(ActionState.Jumping, forced: isClinging);
            shouldJump = false;
        }

        

        
    }

    bool isMovingIntoWall() {
        if (!wallContact) return false;

        float x = controller.GetMoveInput();

        if (x == 0) return false;
        if ((x > 0) == (wallDirection < 0)) return true;
        return false;
    }

    bool isMovingOffWall() {
        if (!wallContact) return false;

        float x = controller.GetMoveInput();

        if (x == 0) return false;
        if ((x < 0) == (wallDirection < 0)) return true;
        return false;
    }


}
