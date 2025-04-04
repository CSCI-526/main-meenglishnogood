using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// public class CheckpointManager : MonoBehaviour
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

public class CheckpointManager : MonoBehaviour
{
    public static CheckpointManager Instance { get; private set; }

    public Vector3 lastCheckpointPosition = new Vector3(0f, 0f, 0f);
    public Vector3 lastLocalScale = new Vector3(0.74f, 0.7f, 1f); // size of the object
    public float lastGravityScale = 1f;

    private void Awake()
    {
        // 
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetGravityScale(float scale)
    {
        Debug.Log("CheckpointManager:Last Gravity Scale recorded: " + scale);
        lastGravityScale = scale;
    }

    public float GetLastGravityScale()
    {
        return lastGravityScale;
    }

    public void SetLastLocalScale(Vector3 localScale)
    {
        lastLocalScale = localScale;
    }

    public Vector3 GetLastLocalScale()
    {
        return lastLocalScale;
    }

    public void SetCheckpoint(Vector3 position)
    {
        lastCheckpointPosition = position;
    }

    public Vector3 GetLastCheckpoint()
    {
        return lastCheckpointPosition;
    }
}
