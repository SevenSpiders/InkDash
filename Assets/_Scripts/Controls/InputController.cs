using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InputController : MonoBehaviour
{
    public abstract float GetMoveInput();
    public abstract bool GetCrouchInput();
    public abstract bool GetJumpInput();
    public abstract bool GetJumpHoldInput();
    public abstract bool GetDashInput();
    public abstract bool GetShootInput();
    public abstract bool GetAttackInput();
}
