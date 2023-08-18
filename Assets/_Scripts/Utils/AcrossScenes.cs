using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcrossScenes : MonoBehaviour
{
    void Awake() => DontDestroyOnLoad(gameObject);
}
