using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour
{
    public GameObject menuUI;

    public void ShowMenu() {
        if (menuUI.activeSelf) {
            menuUI.SetActive(false);
            Time.timeScale = 1f;
        } else {
            menuUI.SetActive(true);
            Time.timeScale = 0f;
        }
    }
}
