using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndScreen : MenuScreen
{
    
    [SerializeField] TMP_Text levelReached;

    void Start() {
        levelReached.text = "You Reached Level " + Game.instance.lastLevel;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            LoadStartScreen();
    }



    public void LoadStartScreen() {
        Game.LoadStartScreen();
    }
}
