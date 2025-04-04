using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartUpdated : MonoBehaviour
{
    public Vector3 restartPoint;

    public void RestartLevel() {
        CheckpointManager.Instance.SetCheckpoint(restartPoint);  // rstart should start from the inital position, not the checkpoint
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
    }
}
