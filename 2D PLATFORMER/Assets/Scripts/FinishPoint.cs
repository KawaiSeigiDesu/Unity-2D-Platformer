using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishPoint : MonoBehaviour
{
    private GameObject gameManager;
    private GameObject gameClearCanvas;
    private GameObject pauseButton;

    private void Start() {
        gameManager = GameObject.FindGameObjectWithTag("Game Manager");
        if (gameManager != null) {
            gameClearCanvas = gameManager.transform.Find("Game Clear Canvas").gameObject;
            pauseButton = gameManager.transform.Find("Pause Canvas").gameObject;
        }
        else {
            Debug.Log("FinishPoint.Start() gameManager == null");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            UnlockNewLevel();
            if (IsExistNextLevel()) {
                SceneController.instance.NextLevel();
            }
            else {
                Time.timeScale = 0f;
                pauseButton.SetActive(false);
                gameClearCanvas.SetActive(true);
            }
        }
    }

    private bool IsExistNextLevel() {
        if (SceneManager.GetActiveScene().buildIndex < SceneManager.sceneCountInBuildSettings - 1) {
            return true;
        }
        return false;
    }

    private void UnlockNewLevel() {
        if (SceneManager.GetActiveScene().buildIndex >= PlayerPrefs.GetInt("ReachedIndex") && IsExistNextLevel()) {
            PlayerPrefs.SetInt("ReachedIndex", SceneManager.GetActiveScene().buildIndex + 1);
            PlayerPrefs.SetInt("UnlockedLevel", PlayerPrefs.GetInt("UnlockedLevel", 1) + 1);
            PlayerPrefs.Save();
        }
    }
}
