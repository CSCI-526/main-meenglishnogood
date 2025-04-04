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

    private Vector3 lastCheckpointPosition;
    private Vector3 lastLocalScale; // size of the object
    private float lastGravityScale;

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
