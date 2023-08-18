using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class AreaTrigger : MonoBehaviour
{

    protected bool isPlayerInside;

    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            isPlayerInside = true;
            OnTrigger();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")) {
            isPlayerInside = false;
        }
    }

    protected virtual void OnTrigger() {}
}
