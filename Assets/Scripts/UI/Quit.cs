using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Quit : MonoBehaviour
{
    public void MainMenu() {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1f;
    }
}
