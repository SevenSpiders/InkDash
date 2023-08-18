using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrouchAction : CharacterAction
{
    bool canCrouch => isRunning || isIdle;

    void Update() {
        if (controller.GetCrouchInput()) {
            if (isCrouching)
                EnterState(ActionState.Idle);
            else if (canCrouch)
                EnterState(ActionState.Crouching);
        }
    }

}
