using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float speed = 2f;
    private Vector3 targetPos;

    public MovementController movementController;
    private Rigidbody2D rb;
    private Vector3 moveDirection;

    private Rigidbody2D playerRb;

    public GameObject ways;
    public Transform[] wayPoints;
    private int pointIndex;
    private int pointCount;
    private int direction = 1;

    public float waitDuration = 0.3f;

    private void Awake() {
        //movementController = GameObject.FindGameObjectWithTag("Player").GetComponent<MovementController>();
        rb = GetComponent<Rigidbody2D>();
        playerRb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();

        wayPoints = new Transform[ways.transform.childCount];
        for (int i = 0; i < ways.gameObject.transform.childCount; i++) {
            wayPoints[i] = ways.transform.GetChild(i).gameObject.transform;
        }
    }

    private void Start() {
        pointIndex = 1;
        pointCount = wayPoints.Length;
        targetPos = wayPoints[1].transform.position;
        DirectionCalculate();
    }

    private void Update() {
        if (Vector2.Distance(transform.position, targetPos) < 0.05f) {
            NextPoint();
        }
    }

    private void NextPoint() {
        transform.position = targetPos;
        moveDirection = Vector3.zero;

        if (pointIndex == pointCount -1) {
            direction = -1;
        }

        if (pointIndex == 0) {
            direction = 1;
        }

        pointIndex += direction;
        targetPos = wayPoints[pointIndex].transform.position;

        StartCoroutine(waitNextPoint());
    }

    IEnumerator waitNextPoint() {
        yield return new WaitForSeconds(waitDuration);
        DirectionCalculate();
    }

    private void FixedUpdate() {
        rb.velocity = moveDirection * speed;
    }

    private void DirectionCalculate() {
        moveDirection = (targetPos - transform.position).normalized;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            playerRb = collision.GetComponent<Rigidbody2D>();
            movementController.isOnPlatform = true;
            movementController.platformRb = rb;
            playerRb.gravityScale *= 50;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            playerRb = collision.GetComponent<Rigidbody2D>();
            movementController.isOnPlatform = false;
            playerRb.gravityScale /= 50;
        }
    }
}
