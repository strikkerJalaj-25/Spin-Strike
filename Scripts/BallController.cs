using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems; // IMPORTANT for UI click blocking

public class BallController : MonoBehaviour
{
    public float moveForce = 8f;
    private bool isMoving = false;

    private Rigidbody rb;
    private RotatingLine rayIndicator;
    private PlayerHealth playerHealth;
    public GameObject LevelCompleteUI;
    public GameObject GameOverUI;

    public AudioSource ballShoot;
    public AudioSource wallHit;
    public AudioSource gameOver;

    private bool levelCompleted = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rayIndicator = GetComponent<RotatingLine>();
        playerHealth = GetComponent<PlayerHealth>();

        if (LevelCompleteUI != null)
            LevelCompleteUI.SetActive(false);

        if (rb == null) Debug.LogError("Rigidbody missing on Ball!");
        if (rayIndicator == null) Debug.LogError("RotatingLine script missing on Ball!");
        if (playerHealth == null) Debug.LogError("PlayerHealth script missing on Ball!");
    }

    void Update()
    {
        if (levelCompleted) return; // Block input if level is complete

        // Stop input if tapping UI (like camera button)
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        // Tap to move if ball is not moving
        if (!isMoving && Input.GetMouseButtonDown(0))
        {
            LaunchBall();
        }

        // If ball slows down automatically
        if (isMoving && rb.velocity.magnitude < 0.05f)
        {
            StopBallMovement();
        }
    }

    void LaunchBall()
    {
        Vector3 direction = rayIndicator.GetDirection();
        direction.y = 0f;

        rb.velocity = direction * moveForce;
        isMoving = true;
        rayIndicator.canAim = false;
        ballShoot.Play();
    }

    void StopBallMovement()
    {
        rb.velocity = Vector3.zero;
        isMoving = false;
        rayIndicator.canAim = true;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!isMoving) return;

        // Hitting walls
        if (collision.gameObject.CompareTag("Wall"))
        {
            StopBallMovement();
            playerHealth.ReduceHealth(1);
            wallHit.Play();

            ParticleScript wall = collision.gameObject.GetComponent<ParticleScript>();
            wall.GameObject();
        
            // Game Over Trigger
            if (playerHealth.currentHealth <= 0)
            {
                GameOverUI.SetActive(true);
                levelCompleted = true;
                rayIndicator.canAim = false; 
                gameOver.Play();
                return;
            }
        }

        // Level finish target
        if (collision.gameObject.CompareTag("Finish"))
        {
            StopBallMovement();
            rayIndicator.canAim = false;
            levelCompleted = true;
            LevelCompleteUI.SetActive(true);
            GameOverUI.SetActive(false);
        }

        // Teleportation
        if (collision.gameObject.CompareTag("Teleportation"))
        {
            TeleportPoint teleport = collision.gameObject.GetComponent<TeleportPoint>();
            if (teleport != null && teleport.targetLocation != null)
            {
                TeleportPlayer(teleport.targetLocation.position);
            }
        }

    }

    void TeleportPlayer(Vector3 newPosition)
    {
        rb.velocity = Vector3.zero;
        isMoving = false;
        rayIndicator.canAim = true;

        // Teleport player
        transform.position = newPosition;

        // Update camera position immediately after teleport
        if (Camera.main != null)
        {
            Camera.main.transform.position = Camera.main.transform.position; // Force refresh if needed
        }
    }

}
