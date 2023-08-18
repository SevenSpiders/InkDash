using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{

    public static Game instance;

    public float ink; // saved across scenes
    public bool skipTutorial;
    public static bool isLevelComplete;

    public int lastLevel;
    public int maxLevelReached;


    void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }


    public static void LoadNextLevel() {
        isLevelComplete = false;

        instance.ink = Player.instance.ink <= 0 ? Player.instance.inkMax : Player.instance.ink;

        // Get the current scene index
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // Calculate the index of the next scene
        int nextSceneIndex = currentSceneIndex + 1;

        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings) {

            instance.lastLevel = currentSceneIndex;
            instance.maxLevelReached = Mathf.Max(instance.maxLevelReached, currentSceneIndex);
            SceneManager.LoadScene(nextSceneIndex);

        } else {
            // The next scene index is out of range, so you can handle it accordingly (e.g., restart the game, go to a specific scene, etc.)
            Debug.LogWarning("No next scene available. Handle this condition accordingly.");
            LoadStartScreen();
            
        }
    }

    public static void ReloadLevel() {
        isLevelComplete = false;
        // instance.ink = Player.instance.ink <= 0 ? Player.instance.inkMax : Player.instance.ink;
        instance.ink = Player.instance.inkMax;

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    public static void LoadStartScreen() {
        isLevelComplete = false;
        instance.ink = 100f; //Player.instance.inkMax;
        SceneManager.LoadScene(0);
    }

    public void LoadGame() {
        isLevelComplete = false;
        int sceneIdx = skipTutorial ? 2 : 1;
        SceneManager.LoadScene(sceneIdx);
    }

}
