using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObstacle : MonoBehaviour
{
    [Range(0,5)]
    public float speed = 2f;
    private Vector3 targetPos;

    private Rigidbody2D rb;
    private Vector3 moveDirection;

    public GameObject ways;
    public Transform[] wayPoints;
    private int pointIndex;
    private int pointCount;
    private int direction = 1;

    [Range(0, 2)]
    public float waitDuration = 1.0f;
    private int speedMultiplier = 1;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();

        wayPoints = new Transform[ways.transform.childCount];
        for (int i = 0; i < ways.gameObject.transform.childCount; i++) {
            wayPoints[i] = ways.transform.GetChild(i).gameObject.transform;
        }
    }

    private void Start() {
        pointCount = wayPoints.Length;
        pointIndex = 1;
        targetPos = wayPoints[1].transform.position;
        DirectionCalculate();
    }

    private void Update() {
        var step = speedMultiplier * speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targetPos, step);
        if (transform.position == targetPos) {
            NextPoint();
        }
    }

    private void NextPoint() {
        transform.position = targetPos;
        moveDirection = Vector3.zero;

        if (pointIndex == pointCount - 1) {
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
        speedMultiplier = 0;
        yield return new WaitForSeconds(waitDuration);
        speedMultiplier = 1;
        DirectionCalculate();
    }

    private void FixedUpdate() {
        rb.velocity = moveDirection * speed;
    }

    private void DirectionCalculate() {
        moveDirection = (targetPos - transform.position).normalized;
    }
}
