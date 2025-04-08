using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowMap : MonoBehaviour
{
    public Transform player;
    public float panDuration = 5f;          //Total duration of camera pan
    public Vector3 leftEdge;                //Camera start position
    public Vector3 rightEdge;               //Camera end position

    private Camera mainCam;
    private Vector3 originalPosition;
    private float originalSize;

    void Start()
    {
        mainCam = Camera.main;
        originalPosition = mainCam.transform.position;
        originalSize = mainCam.orthographicSize;
        StartCoroutine(PanAcrossMap());
    }

    IEnumerator PanAcrossMap()
    {

        float elapsed = 0f;

        float targetSize = originalSize + 4f;
        mainCam.orthographicSize = targetSize;

        //Smoothly move the camera from left to right
        while (elapsed < panDuration)
        {
            float t = elapsed / panDuration;
            Vector3 camPos = Vector3.Lerp(leftEdge, rightEdge, t);
            camPos.z = originalPosition.z;
            mainCam.transform.position = camPos;

            elapsed += Time.deltaTime;
            yield return null;
        }

        //Move camera to player position and restore original size
        //mainCam.transform.position = new Vector3(player.position.x, player.position.y, -4f);
        //mainCam.orthographicSize = originalSize;
        mainCam.transform.position = originalPosition;
        mainCam.orthographicSize = originalSize;
    }
}
