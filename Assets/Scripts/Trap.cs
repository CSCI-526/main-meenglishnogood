using UnityEngine;
using UnityEngine.SceneManagement;

public class Trap : MonoBehaviour
{
    public float restartDelay = 0f; 
    // public GameObject grayOut;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Invoke("RestartGame", restartDelay); 
        }
    }

    void RestartGame()
    {
        // grayOut.SetActive(true);
        if (!IsSceneLoaded("Game_Over")) {
            SceneManager.LoadScene("Game_Over", LoadSceneMode.Additive);
            GameObject SettingsButtonWrapper = GameObject.Find("SettingsButtonWrapper");
            Debug.Log("Found the object = " + SettingsButtonWrapper);
            if (SettingsButtonWrapper != null) {
                SettingsButtonWrapper.SetActive(false);
            }
        }
    }

    bool IsSceneLoaded(string sceneName)
    {
        Scene scene = SceneManager.GetSceneByName(sceneName);
        return scene.IsValid(); 
    }
}
