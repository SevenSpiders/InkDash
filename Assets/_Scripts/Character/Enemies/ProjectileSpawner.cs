using UnityEngine;


public class ProjectileSpawner : MonoBehaviour
{
    [SerializeField] Transform spawnPoint;
    [SerializeField] float offset;
    [SerializeField] float spawnInterval = 1f; // Interval between each spawn

    [Header("Projectile")]
    [SerializeField] Projectile projectilePrefab; 
    [SerializeField] float speed = 20f;
    [SerializeField] float projectileScale = 1f;

    float timer;

    void Start() {
        timer = -offset;
    }

    void Update() {

        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnProjectile(); // Spawn a projectile
            timer = 0; // Reset the timer
        }
    }

    private void SpawnProjectile()
    {
        Projectile projectile = Instantiate(projectilePrefab, spawnPoint.position, spawnPoint.rotation);
        projectile.transform.localScale = projectileScale * Vector3.one;
        projectile.speed = speed;
    }

    void OnDrawGizmos() {
        Gizmos.DrawWireSphere(transform.position, 1f);
    }
}
