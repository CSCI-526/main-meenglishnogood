using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    public void SetUp() {
        gameObject.SetActive(true);
    }

    public void RestartButton() {
        //SceneManager.LoadScene("Level 1");
        string lastLevel = PlayerPrefs.GetString("LastLevel", "Level 1-Beta"); // return the first level in default
        SceneManager.LoadScene(lastLevel);
    }

    public void ExitButton() {
        SceneManager.LoadScene("MainMenu");
    }
}
