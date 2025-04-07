using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
    
    public void StartGame() {
        SceneManager.LoadScene("Level_Selection");
    }
}
