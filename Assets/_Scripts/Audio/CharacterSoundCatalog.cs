using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName="Sounds_", menuName="Audio/Character Sound Catalog")]
public class CharacterSoundCatalog : SoundCatalog
{
    [Header("Character Sounds")]
    public Sound Move;
    public Sound Jump;
    public Sound Dash;
    public Sound Land;

    public Sound Hurt;
    public Sound Death;

    public Sound Attack;

    // for child classes to modify
    public override List<Sound> GetSounds() => GetAllSounds();



    [ContextMenu("Update Sounds")]
    public void UpdateSounds() {
        var soundProperties = typeof(CharacterSoundCatalog).GetFields();
        foreach (var property in soundProperties) {
            Debug.Log(property);
            if (property.FieldType == typeof(Sound))
            {
                var sound = (Sound)property.GetValue(this);
                if (sound != null)
                {
                    sound.name = property.Name;
                    sound.type = (SoundType)System.Enum.Parse(typeof(SoundType), sound.name);
                    sound.details.volume = 1f;
                    sound.details.pitch = 1f;
                }
            }
        }
    }

    List<Sound> GetAllSounds() {
        var soundProperties = typeof(CharacterSoundCatalog).GetFields();
        List<Sound> soundList = new();

        foreach (var property in soundProperties) {

            if (property.FieldType == typeof(Sound)) {
                Sound sound = (Sound)property.GetValue(this);
                if (sound != null)
                    soundList.Add(sound);
            }
        }

        return soundList;
    }

}
