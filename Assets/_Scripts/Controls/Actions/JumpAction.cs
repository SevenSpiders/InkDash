using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpAction : CharacterAction
{
    [SerializeField] float jumpHeight = 3f;
    [SerializeField] int maxAirJumps = 0;
    [SerializeField] float gravityScaleFalling = 6f;
    [SerializeField] float gravityScaleRising = 2f;
    [SerializeField] float coyoteTime = 0.2f;
    [SerializeField] float jumpBufferTime = 0.2f;

    
    DashAction dashAction;
    Vector2 velocity;

    int countAirJumps;
    float defaultGravityScale = 1f;
    float airTime;
    float t_jumpBuffer;

    bool shouldJump;
    bool isGrounded;

    float t_reallyFalling;





    void Awake() {
        dashAction = GetComponent<DashAction>();
    }




    // ------------- UPDATE ---------------
    void Update() {

        if (controller.GetJumpInput() && !shouldJump) {
            shouldJump = true;
            t_jumpBuffer = jumpBufferTime;
        }
    }

    void FixedUpdate() {

        velocity = body.velocity;

        if (!isGrounded) airTime += Time.deltaTime;

        if (contactCheck.groundContact && !isGrounded && (isFalling || isJumping)) {
            LandOnGround();
        }

        isGrounded = contactCheck.groundContact;
        
        if (velocity.y < -1f && !isFalling && !isGrounded) {
            t_reallyFalling += Time.deltaTime;
            if (t_reallyFalling > 0.1f) EnterState(ActionState.Falling);
        } else t_reallyFalling = 0;

        if (isGrounded) {
            if (isJumping && airTime > 0.3f) LandOnGround();

        }

        

        if (shouldJump) {
            TryJump();

            t_jumpBuffer -= Time.deltaTime;
            shouldJump = !isJumping && (t_jumpBuffer > 0);
        }

        // Set Gravity Scale
        if (isDashing)
            body.gravityScale = 0;

        else if (isGrounded)
            body.gravityScale = defaultGravityScale;

        else if (velocity.y > 0 && controller.GetJumpHoldInput())
            body.gravityScale = gravityScaleRising;

        else if (velocity.y <= 0 || !controller.GetJumpHoldInput())
            body.gravityScale = gravityScaleFalling;
        
        
        


        body.velocity = velocity;
    }





    // --------------- ACTION -----------------

    void TryJump() {

        // Ground Jump
        if (isGrounded) {
            velocity.y = 0;
            GroundJump();
        }

        // Coyote Jump
        else if (airTime <= coyoteTime) {
            velocity.y = 0;
            GroundJump();
        }

        // Air Jump
        else if (countAirJumps < maxAirJumps) {
            velocity.y = 0;
            AirJump();
        }
    }


    void GroundJump() {
        float jumpSpeed = Mathf.Sqrt(-2f * Physics2D.gravity.y * jumpHeight);
        if (velocity.y > 0) {
            jumpSpeed = Mathf.Max(jumpSpeed -velocity.y, 0);
        }
        velocity.y += jumpSpeed;
        audioPlayer.Play("Jump");
        EnterState(ActionState.Jumping);
    }

    void AirJump() {
        countAirJumps += 1;
        float jumpSpeed = Mathf.Sqrt(-2f * Physics2D.gravity.y * jumpHeight);
        if (velocity.y > 0) {
            jumpSpeed = Mathf.Max(jumpSpeed -velocity.y, 0);
        }
        velocity.y += jumpSpeed;
        audioPlayer.Play("Jump");
        EnterState(ActionState.Jumping);
    }


    // resets all cooldowns and timers
    void LandOnGround() {
        isGrounded = true;
        countAirJumps = 0;

        // new state
        if (airTime > 1f) EnterState(ActionState.Landing, forced: true);
        else if (Mathf.Abs(velocity.x) > 1f) EnterState(ActionState.Running, forced: true);
        else EnterState(ActionState.Idle, forced: true);

        airTime = 0;
        audioPlayer.Play("Land");
    }

}
