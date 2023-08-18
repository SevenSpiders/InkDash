using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class AudioPlayer : MonoBehaviour
{
    public GameObject audioContainer;
    public SoundCatalog soundCatalog;
    public List<Sound> sounds;

    
    void Awake() {
        if (soundCatalog != null) {
            foreach (Sound sound in soundCatalog.GetSounds()) {
                sounds.Add(sound);
            }
        }

        foreach (Sound sound in sounds) {
            CreateAudioComponent(sound);
        }

        if (audioContainer == null) audioContainer = gameObject;
    }

    void CreateAudioComponent(Sound sound) {
        sound.source = audioContainer.AddComponent<AudioSource>();
        sound.source.clip = sound.clip;
        sound.source.volume = sound.volume;
        sound.source.pitch = sound.pitch == 0 ? 1f : sound.pitch; 
        sound.source.spatialBlend = 1f; // important else no audio falloff in distance
        sound.source.maxDistance = 20f;
        sound.source.rolloffMode = AudioRolloffMode.Linear;
        sound.source.loop = sound.loop;
    } 
    

    public void AddSound(Sound sound) {
        sounds.Add(sound);
        CreateAudioComponent(sound);
    }



    public void Play(SoundType type) {
        Sound s = FindSound(type);
        s?.source?.Play();
    }
    
    public void Play(string name) {
        foreach (Sound s in sounds) if (s.name == name) s.source.Play(); 
    }

    public void Play(string name, AudioParameters parameters) {
        foreach (Sound s in sounds) 
            if (s.name == name) {
                s.source.volume = parameters.volume;
                s.source.pitch = parameters.pitch;
                s.source.Play();
            }
    }

    public void Play(Sound sound) {
        if (sound.source == null) return;
        sound.source.Play();
    }




    public Sound FindSound(string name) {
        for (int i=0; i< sounds.Count; i++) {
            if (sounds[i].name == name) return sounds[i];
        }
        return null;
    }

    public Sound FindSound(SoundType type) {
        for (int i=0; i< sounds.Count; i++) {
            if (sounds[i].type == type) return sounds[i];
        }
        return null;
    }

}


[System.Serializable]
public struct AudioParameters {
    public float pitch;
    public float volume;

    public AudioParameters(float volume, float pitch) {
        this.volume = volume;
        this.pitch = pitch;
    }

    public static AudioParameters one => new AudioParameters(1f, 1f);
}
