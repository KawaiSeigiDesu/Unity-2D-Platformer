using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour
{
    [Header("Movement Particle")]
    [SerializeField] ParticleSystem movementParticle;

    [Range(0,10)]
    [SerializeField] int occurAfterVelocity = 4;

    [Range(0,0.2f)]
    [SerializeField] float dustFormationPeriod = 0.04f;
    [SerializeField] Rigidbody2D playerRb;

    [Header("")]
    [SerializeField] ParticleSystem fallParticle;
    [SerializeField] ParticleSystem touchParticle;
    [SerializeField] ParticleSystem dieParticle;

    private AudioManager audioManager;

    private float counter;
    private bool isOnGround = true;

    private void Awake() {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void Start() {
        touchParticle.transform.parent = null;
    }

    private void Update() {
        counter += Time.deltaTime;

        if(isOnGround && Mathf.Abs(playerRb.velocity.x) > occurAfterVelocity) {
            if (counter > dustFormationPeriod) {
                movementParticle.Play();
                counter = 0;
            }
        }
    }

    public void PlayDieParticle(Vector2 pos) {
        audioManager.PlaySFX(audioManager.death);
        dieParticle.transform.position = pos;
        dieParticle.Play();
    }

    public void PlayTouchParticle(Vector2 pos) {
        audioManager.PlaySFX(audioManager.wallTouch);
        touchParticle.transform.position = pos;
        touchParticle.Play();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Ground")) {
            fallParticle.Play();
            isOnGround = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.CompareTag("Ground")) {
            isOnGround = false;
        }
    }
}
