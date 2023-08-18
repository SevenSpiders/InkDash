using UnityEngine;


public class BallSpawner : MonoBehaviour
{
    [SerializeField] Transform spawnPoint;
    [SerializeField] float timeOffset;
    [SerializeField] float spawnInterval = 1f; // Interval between each spawn

    [Header("Ball")]
    [SerializeField] GameObject ballPrefab; 
    [SerializeField] float projectileScale = 1f;

    float timer;

    void Start() {
        timer = -timeOffset;
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
        GameObject projectile = Instantiate(ballPrefab, spawnPoint.position, spawnPoint.rotation);
        projectile.transform.localScale = projectileScale * Vector3.one;
    }

}
