using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEnd : AreaTrigger
{

    [SerializeField] ParticleSystem vfx;
    AudioSource sound;

    void Awake() {
        sound = GetComponent<AudioSource>();
    }

    protected override void OnTrigger() {
        // Debug.LogWarning("LEVEL ENDED");
        // LoadNextLevel();


        Game.isLevelComplete = true;
        Player.instance.FreezeBody();
        HUD.instance.ShowVictory();

        vfx.Play();
        sound.Play();
    }

}
