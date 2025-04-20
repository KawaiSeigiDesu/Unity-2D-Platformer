using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public GameController gameController;
    public Transform respawnPoint;

    private SpriteRenderer spriteRenderer;
    public Sprite passive, active;
    private Collider2D coll;

    private AudioManager audioManager;

    private void Awake() {
        //gameController = GameObject.FindGameObjectWithTag("Player").GetComponent<GameController>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        coll = GetComponent<Collider2D>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();

    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            audioManager.PlaySFX(audioManager.checkpoint);
            gameController.UpdateCheckPoint(respawnPoint.position, respawnPoint.rotation);
            spriteRenderer.sprite = active;
            coll.enabled = false;
        }
    }
}
