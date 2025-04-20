using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] Text timerText;

    /// <summary>
    /// This is for elapsed and gameover timer
    /// </summary>
    
    // Uncomment This if need elapsed timer
    private float elapsedTime;

    private void Update() {
        elapsedTime += Time.deltaTime;
        int minutes = Mathf.FloorToInt(elapsedTime / 60);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void resetTime() {
        elapsedTime = 0f;
    }

    ////Uncomment This if need gameover timer
    //[SerializeField] float remainingTime;

    //private void Update() {
    //    if (remainingTime > 0) {
    //        remainingTime -= Time.deltaTime;
    //    }
    //    else if (remainingTime < 0) {
    //        remainingTime = 0;
    //        //GameOver();
    //        timerText.color = Color.red;
    //    }
    //    int minutes = Mathf.FloorToInt(remainingTime / 60);
    //    int seconds = Mathf.FloorToInt(remainingTime % 60);
    //    timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    //}

    //private void GameOver() {
    //    // do something
    //}
}
