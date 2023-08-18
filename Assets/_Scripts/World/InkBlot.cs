using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InkBlot : AreaTrigger
{
    [SerializeField] float value;
    [SerializeField] ParticleSystem vfxPrefab;


    protected override void OnTrigger() {
        Player.GainInk(value);
        ParticleSystem vfx = Instantiate(vfxPrefab);
        vfx.transform.position = transform.position;
        vfx.Play();
        Destroy(gameObject);
    }
}
