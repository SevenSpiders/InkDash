using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;


public class HUD : MonoBehaviour
{

    public static HUD instance;

    void Awake() {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    [SerializeField] CharacterActionManager stateManager;
    public Rigidbody2D playerBody;
    [SerializeField] TMP_Text debuggingText;
    [SerializeField] TMP_Text debuggingText2;
    [SerializeField] Image inkFill;
    [SerializeField] GameObject inkBar;

    [SerializeField] GameObject gameOverScreen;
    [SerializeField] GameObject victoryScreen;

    [Header("Sounds")]
    [SerializeField] AudioSource gameOverSound;
    [SerializeField] AudioSource warningSound;

    bool isShaking;
    Tweener shakeTweener;
    float shakeDuration = 0.5f;
    float shakeStrength = 10f;

    bool waitForInput;
    State state = State.Idle;
    enum State {
        Disabled, Idle, LowHealth, GameOver, Vicory 
    }


    void Update() {
        debuggingText.text = stateManager.state.ToString();
        debuggingText2.text = "x: " + playerBody.velocity.x.ToString("F3")+ " y:" + playerBody.velocity.y.ToString("F3");
        UpdateInkBar();

        if (state == State.Vicory) {
            if (Input.GetKeyDown(KeyCode.Space) && waitForInput) LoadNextLevel();
        }

        else if (state == State.GameOver) {
            if (Input.GetKeyDown(KeyCode.Space) && waitForInput) ReloadLevel();
        }
    }

    void UpdateInkBar() {
        inkFill.fillAmount = Player.instance.inkRatio;
        if (inkFill.fillAmount < 0.3f) {
            if (!warningSound.isPlaying) warningSound.Play();
            ShakeHealthbar();
        }
        else {
            warningSound.Stop();
            StopHealthBar();
        }
            
    }

    void ShakeHealthbar() {
        if (isShaking) return;
        isShaking = true;
        shakeTweener = inkBar.transform.DOShakePosition(shakeDuration, shakeStrength)
            .SetEase(Ease.OutQuad)
            .SetLoops(-1);
    }

    void StopHealthBar() {
        isShaking = false;
        if (shakeTweener != null) {
            shakeTweener.Kill();
            // ResetHealthBarPosition();
        }
    }

    public void ShowGameOver() {
        state = State.GameOver;
        Invoke(nameof(SetInputDelay), 1f);
        gameOverSound.Play();
        gameOverScreen.SetActive(true);
    }
    public void HideGameOver() {
        state = State.Idle;
        waitForInput = false;
        gameOverScreen.SetActive(false);
    }

    public void ShowVictory() {
        state = State.Vicory;
        Invoke(nameof(SetInputDelay), 1f);
        victoryScreen.SetActive(true);
    }

    public void HideVictory() {
        state = State.Idle;
        waitForInput = false;
        victoryScreen.SetActive(false);
    }

    void SetInputDelay() {
        waitForInput = true;
    }

    public void LoadNextLevel() {
         waitForInput = false;
        Game.LoadNextLevel();
    }

    public void ReloadLevel() {

         waitForInput = false;
        Game.ReloadLevel();
    }


}
