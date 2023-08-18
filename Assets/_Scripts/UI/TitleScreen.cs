using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TitleScreen : MenuScreen
{
    public bool skipTutorial = true;

    [SerializeField] TMP_Text tutorialSetting;
    [SerializeField] TMP_Text maxLevelReached;
    [SerializeField] AudioSource clickSound;

    void Start() {
        if (Game.instance.maxLevelReached > 0)
            maxLevelReached.text = "Level "+ Game.instance.maxLevelReached.ToString();
        skipTutorial = Game.instance.skipTutorial;
        UpdateTutorialText();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            LoadGame();
    }

    public void ToggleTutorial() {
        skipTutorial = !skipTutorial;
        Game.instance.skipTutorial = skipTutorial;
        UpdateTutorialText();
        clickSound.Play();
    }

    void UpdateTutorialText() {
        tutorialSetting.text = skipTutorial ? "[ ] Tutorial" : "[X] Tutorial";
    }


    public void LoadGame() {
        Game.instance.LoadGame();
    }


}
