using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour
{
    public GameObject menuUI;
    public GameObject grayOut;

    public void ShowMenu() {
        if (menuUI.activeSelf) {
            grayOut.SetActive(false);
            menuUI.SetActive(false);
            Time.timeScale = 1f;
        } else {
            grayOut.SetActive(true);
            menuUI.SetActive(true);
            Time.timeScale = 0f;
        }
    }
}
