using UnityEngine;
using UnityEngine.SceneManagement;

public class Trap : MonoBehaviour
{
    public float restartDelay = 0f; 
    public GameObject grayOut;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Invoke("RestartGame", restartDelay); 
        }
    }

    void RestartGame()
    {
        grayOut.SetActive(true);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
