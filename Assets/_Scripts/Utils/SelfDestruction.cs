using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruction : MonoBehaviour
{
    [SerializeField] float timer = 1f;

    void Start() {
        Invoke(nameof(SelfDestruct), timer);
    }

    public void SelfDestruct() {
        Destroy(gameObject);
    }
}
