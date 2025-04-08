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

    // dictionary for different types of powerup status
    private Dictionary<GameObject, bool> abilityStates = new Dictionary<GameObject, bool>();  // persistent powerup
    private Dictionary<GameObject, bool> shrinkTriStatus = new Dictionary<GameObject, bool>(); // shrink powerup
    private Dictionary<GameObject, bool> growTriStatus = new Dictionary<GameObject, bool>(); // grow powerup


    private GameObject[] changedPersistentPlatforms = new GameObject[0];
    private GameObject[] invisibleWalls;

    private int abilityCount = 0;


    private void Awake()
    {
        // 
        if (Instance == null)
        {
            Debug.Log("Checkpoint Manager instance initiated ");
            Instance = this;
            DontDestroyOnLoad(gameObject); // 

            CheckpointInitialization();

        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void CheckpointInitialization()
    {
        // initialization
        SetLastLocalScale(new Vector3(0.74f, 0.7f, 1f));
        SetGravityScale(1f);
        SetCheckpoint(new Vector3(0f, 0f, 0f));
        SetAbilityCount(0);
        SavePowerupsStates();
        invisibleWalls = GameObject.FindGameObjectsWithTag("InvisibleWall");
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
        Debug.Log("Last Local Scale: " + lastLocalScale);
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

    // Save the status of all powerups
    public void SavePowerupsStates()
    {
        // clear previous data
        abilityStates.Clear();
        shrinkTriStatus.Clear();
        growTriStatus.Clear();

        GameObject[] abilityObjects = GameObject.FindGameObjectsWithTag("Ability");
        GameObject[] shrinkObjects = GameObject.FindGameObjectsWithTag("ShrinkTriangle");
        GameObject[] growObjects = GameObject.FindGameObjectsWithTag("GrowTriangle");

        foreach (GameObject obj in abilityObjects)
        {
            abilityStates[obj] = obj.activeSelf;
        }

        foreach (GameObject obj in shrinkObjects)
        {
            shrinkTriStatus[obj] = obj.activeSelf;
        }

        foreach (GameObject obj in growObjects)
        {
            growTriStatus[obj] = obj.activeSelf;
        }
    }

    // Restore powerup status
    public void RestoreAbilityStates()
    {
        foreach (var kvp in abilityStates)
        {
            if (kvp.Key != null)
                kvp.Key.SetActive(kvp.Value);
        }

        foreach (var kvp in shrinkTriStatus)
        {
            if (kvp.Key != null)
                kvp.Key.SetActive(kvp.Value);
        }

        foreach (var kvp in growTriStatus)
        {
            if (kvp.Key != null)
                kvp.Key.SetActive(kvp.Value);
        }
    }


    // get the platforms that are changed into persistent
    public void GetChangedPlatforms()
    {
        changedPersistentPlatforms = new GameObject[0];
        changedPersistentPlatforms = GameObject.FindGameObjectsWithTag("PersistentBlock");

        invisibleWalls = new GameObject[0];
        invisibleWalls = GameObject.FindGameObjectsWithTag("InvisibleWall");

    }


    // Destroy persistent blocks, make all invisible walls active
    public void RecoverChangedPlatforms()
    {
        // destroy persistent block
        foreach (GameObject persistentBlock in changedPersistentPlatforms)
        {
            if (persistentBlock!=null && persistentBlock.activeSelf)
            {
                Destroy(persistentBlock);
            }
        }

        // make all invisible walls active
        foreach (GameObject wall in invisibleWalls)
        {
            if (wall!=null && !wall.activeSelf)
            {
                Debug.Log("Recover changed platforms");
                wall.SetActive(true);
            }
        }

    }

    public void SetAbilityCount(int count)
    {
        abilityCount = count;
    }

    public int GetAbilityCount()
    {
        return abilityCount;
    }

}
