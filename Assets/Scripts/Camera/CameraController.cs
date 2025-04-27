using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    private Vector3 offset = new Vector3(2f, 1f, -10f); // offset when following player


    
    public bool isLockX = false;
    public bool isLockY = false;
    public float moveSpeed = 2.0f;
    public float sizeChangeSpeed = 2.0f;

    private Vector3 targetPos; // transform hope the camera can get to
    private float targetSize = 7.0f;

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
        if (isTransitioning) return;
        if (player != null && !isLockTrigger)
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

    public void TriggerCameraLockBehavior(float sizeIn, Vector3 targetPosIn)
    {
        //if(fixedXIn != 0)
        //{
        //    fixedX = fixedX;
        //}
        //if(fixedYIn != 0)
        //{
        //    fixedY = fixedYIn；
        //}
        Debug.Log("Camera: position changed");
        targetSize = sizeIn;
        targetPos = new Vector3(player.position.x, targetPosIn.y, targetPosIn.z);
        

        isTransitioning = true;
        isLockTrigger = true;

        StartCoroutine(LerpCameraToTarget());
    }

    public void CameraFollowPlayer()
    {
        
        isLockTrigger = false;
        targetSize = 5;
        targetPos = player.position;
        //isTransitioning = false;
        
        Debug.Log("Camera: camera follow player, isLockTrigger: " + isLockTrigger);
        StartCoroutine(LerpCameraToTarget());
    }


    private IEnumerator LerpCameraToTarget()
    {
        // move camera to target transform
        while (Mathf.Abs(cam.orthographicSize - targetSize) > 0.01f ||
              Vector3.Distance(transform.position, targetPos) > 0.01f)
        {
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, targetSize, sizeChangeSpeed * Time.deltaTime);
            transform.position = Vector3.Lerp(transform.position, targetPos, moveSpeed * Time.deltaTime);

            //cam.orthographicSize = Mathf.MoveTowards(cam.orthographicSize, targetSize, sizeChangeSpeed * Time.deltaTime);
            //transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
            yield return null;
        }



            transform.position = targetPos;
            cam.orthographicSize = targetSize;
            fixedY = transform.position.y;
            fixedX = targetPos.x;
            isTransitioning = false;
            Debug.Log("Camera: Reached Lerp Position");
            yield break; // 结束这个协程
    }
}
