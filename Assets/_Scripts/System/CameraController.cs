using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;

    [Header("Follow")]
    [SerializeField] Player player;         // Reference to the player's transform
    [SerializeField] float smoothSpeed = 0.3f; // Smoothing speed of the camera movement
    [SerializeField] float horizontalOffset = 2f; // Additional horizontal offset for camera
    [SerializeField] Vector3 offset;           // Offset distance between the player and the camera

    
    Transform target;
    private Vector3 velocity = Vector3.zero;
    float velocityX, lastVelocity;


    void Awake() {
        if (instance == null) instance = this;
        else {
            Destroy(gameObject);
        }
    }


    

    void Start() {
        target = player.transform;
    }


    // FOLLOW 
    void LateUpdate()
    {
        // Calculate the desired position for the camera
        Vector3 desiredPosition = target.position + offset;
        // Apply additional horizontal offset based on player's movement direction
        
        lastVelocity = velocityX;
        velocityX = target.GetComponent<Rigidbody2D>().velocity.x;
        if (Mathf.Abs(velocityX) < 0.1f) velocityX = lastVelocity;

        float horizontalDirection = Mathf.Sign(velocityX);
        desiredPosition.x += horizontalDirection * horizontalOffset;

        // Smoothly move the camera towards the desired position
        Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothSpeed);

        smoothedPosition.y = desiredPosition.y;
        // Update the camera's position
        transform.position = smoothedPosition;
    }



}
