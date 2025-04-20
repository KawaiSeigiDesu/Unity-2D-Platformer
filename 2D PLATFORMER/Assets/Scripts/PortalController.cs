using System.Collections;
using UnityEngine;

public class PortalController : MonoBehaviour {
    public Transform destination;
    public GameObject player;
    public Animation anim;
    private Rigidbody2D playerRb;

    private AudioManager audioManager;

    private void Awake() {
        //player = GameObject.FindGameObjectWithTag("Player");
        //anim = player.GetComponentInChildren<Animation>();
        playerRb = player.GetComponent<Rigidbody2D>();

        //if (anim == null) {
        //    Debug.LogError("Animation component not found on playerModel!");
        //}
        //else {
        //    Debug.Log("Loaded Animations:");
        //    foreach (AnimationState state in anim) {
        //        Debug.Log(state.name);
        //    }
        //}
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();

    }


    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            if (Vector2.Distance(player.transform.position, transform.position) > 0.3f) {
                StartCoroutine(PortalIn());
            }
        }
    }

    IEnumerator PortalIn() {
        audioManager.PlaySFX(audioManager.portalOut);
        audioManager.PlaySFX(audioManager.portalIn);
        anim.Play("Portal In");
        playerRb.simulated = false;
        StartCoroutine(MoveInPortal());
        yield return new WaitForSeconds(0.5f);
        player.transform.position = destination.position;
        playerRb.velocity = Vector2.zero;
        anim.Play("Portal Out");
        yield return new WaitForSeconds(0.5f);
        playerRb.simulated = true;
    }

    IEnumerator MoveInPortal() {
        float timer = 2;
        while (timer < 0.5f) {
            player.transform.position = Vector2.MoveTowards(player.transform.position, transform.position, 3 * Time.deltaTime);
            yield return new WaitForEndOfFrame();
            timer += Time.deltaTime;
        }
    }
}
