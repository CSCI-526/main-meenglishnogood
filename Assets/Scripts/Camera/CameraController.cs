using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    private Vector3 offset = new Vector3(2f, 1f, -10f); // offset when following player


    public Vector3 targetPos; // transform hope the camera can get to
    public bool isLockX = false;
    public bool isLockY = false;
    public float moveSpeed = 2.0f;
    public float sizeChangeSpeed = 2.0f;
    public float targetSize = 7.0f;

    private Camera cam;
    private bool isTransitioning = false;
    private float fixedY;          
    private float fixedX;
    private float startSize;
    private bool isLockTrigger;

    void Start()
    {
        fixedY = transform.position.y;
        fixedX = transform.position.x;
        cam = GetComponent<Camera>();
        startSize = cam.orthographicSize; // initial size
    }

    void LateUpdate()
    {
        if (isTransitioning)
        {
            // move camera to target transform
            transform.position = Vector3.Lerp(transform.position, targetPos, moveSpeed * Time.deltaTime);

            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, targetSize, sizeChangeSpeed * Time.deltaTime);

            bool positionReached = Vector3.Distance(transform.position, targetPos) < 0.05f;
            bool sizeReached = Mathf.Abs(cam.orthographicSize - targetSize) < 0.05f;

            // if is close enough
            if (positionReached && sizeReached)
            {
                transform.position = targetPos;
                cam.orthographicSize = targetSize;
                fixedY = transform.position.y;
                fixedX = targetPos.x;
                isTransitioning = false;
                Debug.Log("Camera: Reached Lerp Position");
            }
        }
        else if (player != null && !isLockTrigger)
        {
            transform.position = player.position + offset;
        }
        else if (player!=null && isLockTrigger)
        {
            if (isLockX && !isLockY)
            {
                transform.position = new Vector3(fixedX, player.position.y, transform.position.z);
            }
            else if (isLockY && !isLockX)
            {
                // only follow player x axis
                transform.position = new Vector3(player.position.x, fixedY, transform.position.z);
            }

            else if (isLockX && isLockY)
            {
                transform.position = new Vector3(fixedX, fixedY, transform.position.z);
            }
        }

  
    }

    public void TriggerCameraLockBehavior()
    {
        isTransitioning = true;
        isLockTrigger = true;
    }
}
