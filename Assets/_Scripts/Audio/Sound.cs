using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Sound
{
    
    string Name;
    public string name;
    public SoundType type;
    public AudioClip clip;

    // Details
    public AudioDetails details;

    public float volume => details.volume;
    public float pitch => details.pitch;
    public bool loop => details.loop;

    [HideInInspector] public AudioSource source;
}

[System.Serializable]
public struct AudioDetails {
    
    public float volume;
    public float pitch;
    public bool loop;
}
