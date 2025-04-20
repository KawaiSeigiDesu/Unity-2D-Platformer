using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController instance;
    [SerializeField] Animator transitionAnim;
    [SerializeField] private Timer _timer;

    private void Awake() {
        CheckInstance();
    }

    private void CheckInstance() {
        if (instance == null && !SceneManager.GetActiveScene().name.Equals("Main Menu")) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(gameObject);
        }
    }

    public void NextLevel() {
        StartCoroutine(LoadLevel());
    }

    IEnumerator LoadLevel() {
        transitionAnim.SetTrigger("End");
        yield return new WaitForSeconds(0.5f);
        transitionAnim.SetTrigger("Start");
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        _timer.resetTime();
    }

    public void LoadScene(string sceneName) {
        CheckInstance();
        SceneManager.LoadSceneAsync(sceneName);
    }
}
