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

    void Start()
    {
        mainCam = Camera.main;
        StartCoroutine(PanAcrossMap());
    }

    IEnumerator PanAcrossMap()
    {

        float elapsed = 0f;

        float originalSize = mainCam.orthographicSize;
        //Set a larger size for the map overview
        float targetSize = originalSize + 4f;
        mainCam.orthographicSize = targetSize;

        //Smoothly move the camera from left to right
        while (elapsed < panDuration)
        {
            float t = elapsed / panDuration;
            Vector3 camPos = Vector3.Lerp(leftEdge, rightEdge, t);
            camPos.z = -10f;
            mainCam.transform.position = camPos;

            elapsed += Time.deltaTime;
            yield return null;
        }

        //Move camera to player position and restore original size
        mainCam.transform.position = new Vector3(player.position.x, player.position.y, -10f);
        mainCam.orthographicSize = originalSize;
    }
}
