using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAction : CharacterAction
{
    [SerializeField] float maxSpeed = 4f;
    [SerializeField] float maxAcceleration = 35f;
    [SerializeField] float maxAirAcceleration = 35f;


    Vector2 direction;
    Vector2 desiredVelocity;
    Vector2 velocity;


    float maxSpeedChange;
    float acceleration;
    bool isGrounded;
    bool isStuckToWall;


    
    public void StickToWall(bool b) => isStuckToWall = b;


    void Update() {
        direction.x = controller.GetMoveInput();
        UpdateOrientation();
        UpdateState();
        desiredVelocity = new Vector2(direction.x, 0)*Mathf.Max(maxSpeed - contactCheck.friction, 0);

    }

    void UpdateOrientation() {
        if (direction.x == 0) return;
        if ((direction.x > 0) != manager.isFacingRight && !isClinging) manager.Flip();
    }

    void UpdateState() {
        if (direction.x == 0 && isRunning) EnterState(ActionState.Idle);
        else if (Mathf.Abs(direction.x) > 0.1f && !isRunning && isGrounded) 
            EnterState(ActionState.Running); 
    }

    void FixedUpdate() {
        isGrounded = contactCheck.groundContact;
        velocity = body.velocity;


        if (isStuckToWall) { velocity.x = 0; }
        else {
            acceleration = isGrounded ? maxAcceleration : maxAirAcceleration;
            maxSpeedChange = acceleration * Time.deltaTime;
            velocity.x = Mathf.MoveTowards(velocity.x, desiredVelocity.x, maxSpeedChange);

        }

        body.velocity = velocity;
    }


    
}
