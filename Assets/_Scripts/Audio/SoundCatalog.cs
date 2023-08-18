using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="Sounds_", menuName="Audio/Sound Catalog")]
public class SoundCatalog : ScriptableObject
{
    [SerializeField] protected List<Sound> sounds;

    // for child classes to modify
    public virtual List<Sound> GetSounds() => sounds;
}


