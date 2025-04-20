using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    [SerializeField, Range(0f, 1f)]
    public float smoothTime = 0.2f;  // Smooth time for camera movement

    public Vector3 positionOffset = new Vector3(0, 0, -10);  // Camera's offset from the player
    private Vector3 velocity = Vector3.zero;

    [Header("Axis Limitation")]
    public Vector2 xLimit = new Vector2(1.5f, 21.49f);  // X axis limitation (Min, Max)
    public Vector2 yLimit = new Vector2(-3.16f, 0.33f);  // Y axis limitation (Min, Max)

    private Transform player;
    private Transform mainCamera;

    private void Awake() {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").transform;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void LateUpdate() {
        if (player == null || mainCamera == null) return;

        Vector3 targetPosition = player.position + positionOffset;

        targetPosition.x = Mathf.Clamp(targetPosition.x, xLimit.x, xLimit.y);
        targetPosition.y = Mathf.Clamp(targetPosition.y, yLimit.x, yLimit.y);

        mainCamera.position = Vector3.SmoothDamp(mainCamera.position, targetPosition, ref velocity, smoothTime);
    }
}
