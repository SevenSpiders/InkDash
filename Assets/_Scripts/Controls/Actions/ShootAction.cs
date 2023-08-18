using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootAction : CharacterAction
{
    [SerializeField] Transform firePoint;
    [SerializeField] GameObject projectilePrefab;

    bool canShoot => isRunning || isIdle;

    void Update() {
        if (controller.GetShootInput() && canShoot) {
            Shoot();
        }
    }

    void Shoot() {
        Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        audioPlayer.Play(SoundType.Attack);
    }
}
