using System;
using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour
{
    private float maxHealth = 100;
    private float currentHealth;
    [SerializeField] private Image healthBarFill;
    [SerializeField] private GameController gameController;
    [SerializeField] private float damageAmount, healthAmount;
    [SerializeField] private Transform healthBarTransform;
    private Camera _camera;

    private void Awake() {
        currentHealth = maxHealth;
        _camera = Camera.main;
    }

    private void Update() {
        healthBarTransform.rotation = _camera.transform.rotation;
    }

    private void TakeDamage(float amount) {
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        if (currentHealth == 0) {
            gameController.Die();
            currentHealth = maxHealth;
        }
        UpdateHealthBar();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Obstacle")) {
            TakeDamage(damageAmount);
        }
        else if (collision.CompareTag("Health")) {
            Heal(healthAmount);
            collision.gameObject.SetActive(false);
        }
        else if (collision.CompareTag("Deathzone")) {
            gameController.Die();
        }
    }

    private void Heal(float amount) {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0 , maxHealth);
        UpdateHealthBar();
    }

    public void UpdateHealthBar() {
        healthBarFill.fillAmount = currentHealth / maxHealth;
    }
}
