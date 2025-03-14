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
        int lastLevel = PlayerPrefs.GetInt("LastLevel", 1); // Ĭ�Ϸ��ص�һ��
        SceneManager.LoadScene(lastLevel);
    }

    public void ExitButton() {
        SceneManager.LoadScene("MainMenu");
    }
}
