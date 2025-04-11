using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// public class NewBehaviourScript : MonoBehaviour
// {
//     // Start is called before the first frame update
//     void Start()
//     {
        
//     }

//     // Update is called once per frame
//     void Update()
//     {
        
//     }
// }
// using UnityEngine;

// public class SettingsPanelController : MonoBehaviour
// {
//     public GameObject settingsPanel;

//     public void ClosePanel()
//     {
//         settingsPanel.SetActive(false);
//         Debug.Log(" Settings panel closed.");
//     }
// }
using UnityEngine;

public class SettingsPanelController : MonoBehaviour
{
    public GameObject settingsPanel;     // SettingsUI 面板
    public GameObject grayOutOverlay;    // 灰布遮罩面板

    public void OpenPanel()
    {
        settingsPanel.SetActive(true);
        grayOutOverlay.SetActive(true);
        Time.timeScale = 0f; // 暂停游戏
        Debug.Log("Settings opened, game paused.");
    }

    public void ClosePanel()
    {
        settingsPanel.SetActive(false);
        grayOutOverlay.SetActive(false);
        Time.timeScale = 1f; // 恢复游戏
        Debug.Log("Settings closed, game resumed.");
    }
}
