using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class GameController : MonoBehaviour {
    private Vector2 checkpointPosition;
    private Quaternion checkpointRotation;
    private Rigidbody2D playerRb;
    private SpriteRenderer playerRenderer;
    private ShadowCaster2D playerShadowCaster;
    private TrailRenderer playerTrailRenderer;
    private GameObject _healthBarCanvas;
    public ParticleController particleController;

    [SerializeField] private MovementController movementController; // Reference to MovementController
    [SerializeField] private float defaultSpeed = 8.0f; // Adjust this to your desired starting speed

    private void Awake() {
        playerRb = GetComponent<Rigidbody2D>();
        playerRenderer = GetComponentInChildren<SpriteRenderer>(); // Get the Renderer component (e.g., SpriteRenderer)
        playerShadowCaster = GetComponent<ShadowCaster2D>();
        playerTrailRenderer = GetComponent<TrailRenderer>();
        _healthBarCanvas = gameObject.transform.Find("Health Bar Canvas").gameObject;
    }

    private void Start() {
        checkpointPosition = transform.position;
        checkpointRotation = transform.rotation;
    }


    public void Die() {
        _healthBarCanvas.SetActive(false);
        particleController.PlayDieParticle(transform.position);
        StartCoroutine(Respawn(0.5f));

    }

    public void UpdateCheckPoint(Vector2 currentPosition, Quaternion currentRotation) {
        checkpointPosition = currentPosition;
    }

    IEnumerator Respawn(float duration) {
        playerRb.simulated = false;
        playerRb.velocity = Vector2.zero;

        // Make the player invisible by disabling the Renderer
        if (playerRenderer != null) playerRenderer.enabled = false;
        if (playerShadowCaster != null) playerShadowCaster.enabled = false;
        if (playerTrailRenderer != null) playerTrailRenderer.enabled = false;

        yield return new WaitForSeconds(duration);

        // Respawn the player at the starting position
        transform.position = checkpointPosition;

        // Make the player visible again
        if (playerRenderer != null) playerRenderer.enabled = true;
        if (playerShadowCaster != null) playerShadowCaster.enabled = true;
        if (playerTrailRenderer != null) playerTrailRenderer.enabled = true;

        playerRb.simulated = true;

        if (movementController != null) {
            movementController.ResetSpeed(defaultSpeed);
            movementController.CheckAndFlip(checkpointRotation); // Check if the player needs to be flipped
        }

        _healthBarCanvas.SetActive(true);
    }
}
