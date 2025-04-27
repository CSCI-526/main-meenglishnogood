using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLock : MonoBehaviour
{
    public Transform player;       
    public Vector3 targetLocalPos; // Local transform hope the camera can get to
    public bool isLockX = false;
    public bool isLockY = false;
    public float moveSpeed = 2.0f;
    public float sizeChangeSpeed = 2.0f;
    public float targetSize = 7.0f;

    private Camera cam;
    private bool isTransitioning = false;
    private float fixedLocalY;          // 记录初始Camera相对于Player的Y
    private float fixedWorldY;          // 记录当时Camera在世界坐标的Y
    private float fixedX;
    private float startSize;
    private bool isTrigger;

    void Start()
    {
        fixedLocalY = transform.localPosition.y;
        fixedWorldY = transform.position.y;
        fixedX = transform.localPosition.x;
        cam = GetComponent<Camera>();
        startSize = cam.orthographicSize; // initial size
    }

    void LateUpdate()
    {
        if (isTransitioning)
        {
            // move camera to target transform
            transform.localPosition = Vector3.Lerp(transform.localPosition, targetLocalPos, moveSpeed * Time.deltaTime);

            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, targetSize, sizeChangeSpeed * Time.deltaTime);

            bool positionReached = Vector3.Distance(transform.localPosition, targetLocalPos) < 0.05f;
            bool sizeReached = Mathf.Abs(cam.orthographicSize - targetSize) < 0.05f;

            // if is close enough
            if (positionReached && sizeReached)
            {
                transform.localPosition = targetLocalPos;
                cam.orthographicSize = targetSize;
                fixedLocalY = transform.localPosition.y;
                fixedWorldY = transform.position.y;
                fixedX = targetLocalPos.x;
                isTransitioning = false;                                    
            }
        }

        if (isTrigger)
        {
            if (isLockX && !isLockY)
            {
                // only follow player x axis
                transform.localPosition = new Vector3(player.localPosition.x, fixedLocalY, transform.localPosition.z);
            }
            else if (isLockY && !isLockX)
            {
                Vector3 currentLocal = transform.localPosition;
                float offsetY = player.position.y - fixedWorldY;
                transform.localPosition = new Vector3(player.localPosition.x, fixedLocalY - offsetY, currentLocal.z);
                //transform.localPosition = new Vector3(fixedX, player.localPosition.y, transform.localPosition.z);
            }

            else if (isLockX && isLockY)
            {
                transform.localPosition = new Vector3(fixedX, fixedLocalY, transform.localPosition.z);
            }
        }

        
    }

    public void TriggerCameraLockBehavior()
    {
        isTransitioning = true;
    }
}
