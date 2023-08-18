using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake instance;

    [Header("Shake")]
    [SerializeField] private float shakeDuration = 0.3f;
    [SerializeField] private float shakeIntensity = 0.1f;

   
    Vector3 initialPosition;
    bool isShaking;

    
    void Awake() {
        if (instance == null) {
            instance = this;
            initialPosition = transform.localPosition;
        }
        else {
            Destroy(gameObject);
        }


    }


    // SHAKE
    public static void Shake() {
        if (instance == null) return;
        instance.StartShake();
    }

    public void StartShake() {
        instance.StartCoroutine(ShakeCoroutine());
    }

    

    IEnumerator ShakeCoroutine() {
        if (isShaking) yield break;

        float elapsed = 0f;

        while (elapsed < shakeDuration)
        {
            float x = Random.Range(-1f, 1f) * shakeIntensity;
            float y = Random.Range(-1f, 1f) * shakeIntensity;

            transform.localPosition = initialPosition + new Vector3(x, y, 0f);

            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = initialPosition;
    }
}
