using UnityEngine;
using UnityEngine.SceneManagement;

// public class Trap : MonoBehaviour
// {
//     public float restartDelay = 0f;

//     private void OnCollisionEnter2D(Collision2D collision)
//     {
//         if (collision.collider.CompareTag("Player"))
//         {
//             Invoke("RespawnPlayer", restartDelay);
//         }
//     }

//     void RespawnPlayer()
//     {
//         GameObject player = GameObject.FindGameObjectWithTag("Player");
//         if (player != null)
//         {
//             var controller = player.GetComponent<PlayerController>();
//             if (controller != null)
//             {
//                 controller.Respawn();
//                 Debug.Log("🟦 Trap triggered respawn.");
//             }
//         }
//     }
// }

// public class Trap : MonoBehaviour
// {
//     public float restartDelay = 0f; 
//     // public GameObject grayOut;


//     private void OnCollisionEnter2D(Collision2D collision)
//     {
//         if (collision.collider.CompareTag("Player"))
//         {
//             PlayerPrefs.SetInt("LastLevel", SceneManager.GetActiveScene().buildIndex);
//             PlayerPrefs.Save();

//             Invoke("RestartGame", restartDelay); 
//         }
//     }

//     void RestartGame()
//     {
//         // grayOut.SetActive(true);
//         if (!IsSceneLoaded("Game_Over")) {
//             SceneManager.LoadScene("Game_Over", LoadSceneMode.Additive);
//             //GameObject SettingsButtonWrapper = GameObject.Find("SettingsButtonWrapper");
//             GameObject SettingsButtonWrapper = GameObject.Find("Canvas")?.transform.Find("SettingsButtonWrapper")?.gameObject;
//             Debug.Log("Found the object = " + SettingsButtonWrapper);
//             if (SettingsButtonWrapper != null) {
//                 SettingsButtonWrapper.SetActive(false);
//             }
//         }
//     }

//     void RespawnPlayer()
//     {
//         GameObject player = GameObject.FindGameObjectWithTag("Player");
//         if (player != null)
//         {
//             var controller = player.GetComponent<PlayerController>();
//             if (controller != null)
//             {
//                 controller.Respawn();
//                 Debug.Log("🟦 Trap triggered respawn.");
//             }
//         }
//     }
//     bool IsSceneLoaded(string sceneName)
//     {
//         Scene scene = SceneManager.GetSceneByName(sceneName);
//         return scene.IsValid(); 
//     }
// }


public class Trap : MonoBehaviour
{
    public float restartDelay = 0f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            // 
            PlayerPrefs.SetString("LastLevel", UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
            PlayerPrefs.Save();

            Invoke("RespawnPlayer", restartDelay); // 
            //Invoke("RestartGame", restartDelay); // 

        }
    }

    void RespawnPlayer()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            var controller = player.GetComponent<PlayerController>();
            if (controller != null)
            {
                controller.Respawn();
                Debug.Log("Trap triggered respawn.");
            }
        }
    }

    void RestartGame()
    {
        if (!IsSceneLoaded("Game_Over"))
        {
            SceneManager.LoadScene("Game_Over", LoadSceneMode.Additive);
            GameObject SettingsButtonWrapper = GameObject.Find("Canvas")?.transform.Find("SettingsButtonWrapper")?.gameObject;
            if (SettingsButtonWrapper != null)
            {
                SettingsButtonWrapper.SetActive(false);
            }
        }
    }

    bool IsSceneLoaded(string sceneName)
    {
        var scene = UnityEngine.SceneManagement.SceneManager.GetSceneByName(sceneName);
        return scene.IsValid();
    }
}
