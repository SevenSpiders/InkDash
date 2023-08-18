using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InkTrail : MonoBehaviour
{
    [SerializeField] TrailRenderer inkTrail;
    [SerializeField] TrailRenderer dashTrail;

    [SerializeField] CharacterActionManager action;


    void Awake() {
        action.o_Dash += HandleDash;
    }


    void HandleDash(bool start) {
        // print($"dash {start}");
        inkTrail.emitting = !start;
        dashTrail.emitting = start;
    }

}
