using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTrigger : MonoBehaviour
{
    public Vector3 targetPos = new Vector3(34.59f, 3.66f, -7f); // transform hope the camera can get to
    public float targetSize = 7.0f;
    public bool backToOrgPos = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            CameraController cameraController = Camera.main.GetComponent<CameraController>();
            if (!backToOrgPos)
            {
                cameraController.TriggerCameraLockBehavior(targetSize, targetPos);
            }
            else
            {
                cameraController.CameraFollowPlayer();
            }
            
        }
    }
}
