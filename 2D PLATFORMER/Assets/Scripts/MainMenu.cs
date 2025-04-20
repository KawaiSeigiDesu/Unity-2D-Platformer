using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Button[] buttons;
    public GameObject levelButtons;

    private void Awake() {
        ButtonsToArray();
        int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);
        for (int i = 0; i < buttons.Length; i++) {
            buttons[i].interactable = false;
        }
        for (int i = 0; i < unlockedLevel; i++) {
            buttons[i].interactable = true;
        }
    }

    public void PlayGame() {
        SceneManager.LoadSceneAsync(1);
    }

    public void OpenLevel(int levedId) {
        string levelName = "Level " + levedId;
        SceneManager.LoadScene(levelName);
    }

    public void QuitGame() {
        Application.Quit();
    }

    private void ButtonsToArray() {
        int pagesCount = levelButtons.transform.childCount; // Get number of Pages
        List<Button> buttonList = new List<Button>();

        for (int i = 0; i < pagesCount; i++) {
            Transform page = levelButtons.transform.GetChild(i); // Get each Page
            int levelCount = page.childCount; // Get number of Levels inside the Page

            for (int j = 0; j < levelCount; j++) {
                Button btn = page.GetChild(j).GetComponent<Button>();
                if (btn != null) {
                    buttonList.Add(btn);
                }
                else {
                    Debug.LogWarning($"No Button component found on {page.GetChild(j).name}");
                }
            }
        }

        buttons = buttonList.ToArray(); // Convert list to array
    }

    public static void DebugMe() {
        Debug.Log("MainMenu.DebugMe() --Method Called--");
    }
}
