using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : InputController
{
    public override float GetMoveInput() {
        return Input.GetAxisRaw("Horizontal");
    }

    public override bool GetCrouchInput() {
        return Input.GetButtonDown("Down"); // point down
    }


    public override bool GetJumpInput() {
        return Input.GetButtonDown("Jump");
    }

    public override bool GetJumpHoldInput() {
        return Input.GetButton("Jump");
    }
    
    public override bool GetDashInput() {
        return Input.GetButtonDown("Dash");
    }

    public override bool GetShootInput() {
        return Input.GetButtonDown("Fire1");
    }

    public override bool GetAttackInput() {
        return Input.GetButtonDown("Fire2");
    }
}
