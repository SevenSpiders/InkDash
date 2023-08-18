using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{

    [SerializeField] float inkDrain = 1f;

    void Start() {

        // Levels define inkdrain speed
        // Player.instance.inkDrain = inkDrain;
        Player.instance.ink = Game.instance.ink;
    }



    void Update()
    {
        
    }
}
