using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementController : MonoBehaviour {
    private Rigidbody2D rb;
    private SpriteRenderer sr;

    [SerializeField] int speed;
    float speedMultiplier;

    [Range(1, 10)]
    [SerializeField] float acceleration;

    private bool btnPressed;
    private bool isWallTouch;

    public LayerMask wallpaper;
    public Transform wallCheckPoint;
    public Transform playerModel;  // Reference to PlayerModel

    private Vector2 relativeTransform;

    public bool isOnPlatform;
    public Rigidbody2D platformRb;
    public ParticleController particleController;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    private void Start() {
        UpdateRelativeTransform();
    }

    private void FixedUpdate() {
        UpdateSpeedMultiplier();

        float targetSpeed = speed * speedMultiplier * relativeTransform.x;

        if (isOnPlatform) {
            rb.velocity = new Vector2(targetSpeed + platformRb.velocity.x, rb.velocity.y);
        }
        else {
            rb.velocity = new Vector2(targetSpeed, rb.velocity.y);
        }

        isWallTouch = Physics2D.OverlapBox(wallCheckPoint.position, new Vector2(0.06f, 0.5f), 0, wallpaper);

        if (isWallTouch) {
            particleController.PlayTouchParticle(wallCheckPoint.position);
            Flip();
        }
    }

    public void Flip() {
        playerModel.Rotate(0, 180, 0); // Only rotate the visual model, not the entire player
        UpdateRelativeTransform();
    }

    private void UpdateRelativeTransform() {
        relativeTransform = playerModel.InverseTransformVector(Vector2.one);
    }

    public void Move(InputAction.CallbackContext context) {
        if (context.phase == InputActionPhase.Started) {
            btnPressed = true;
        }
        else if (context.phase == InputActionPhase.Canceled) {
            btnPressed = false;
        }
    }

    private void UpdateSpeedMultiplier() {
        if (btnPressed && speedMultiplier < 1) {
            speedMultiplier += Time.deltaTime;
        }
        else if (!btnPressed && speedMultiplier > 0) {
            speedMultiplier -= Time.deltaTime;
            if (speedMultiplier < 0) { speedMultiplier = 0; }
        }
    }

    public void CheckAndFlip(Quaternion checkpointRotation) {
        if (playerModel.rotation != checkpointRotation) {
            Flip(); // Assuming you already have a Flip() method
        }
    }

    public void ResetSpeed(float defaultSpeed) {
        speed = Mathf.RoundToInt(defaultSpeed); // Reset speed to default
        speedMultiplier = 0; // Reset multiplier to prevent instant movement after respawn
    }
}
