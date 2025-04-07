using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectButton : MonoBehaviour
{
    public string levelName = "Level1_Alpha";

    public void SelectScene() {
        SceneManager.LoadScene(levelName);
        Time.timeScale = 1f;
    }
}
