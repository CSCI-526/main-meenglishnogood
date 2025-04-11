using System.Collections;
using UnityEngine;
using static Constants;

public class MainCameraController : MonoBehaviour {

    private Camera mainCamera;
    private Vector3 leftEdge;
    private Vector3 rightEdge;
    
    private void Start() {
        mainCamera = GetComponent<Camera>();
        LevelManager.Instance.mainCamera = mainCamera;
        CalculateCameraBoundsFromScene();
        StartCoroutine(PanAcrossMap());
    }

    private void CalculateCameraBoundsFromScene() {
        var renderers = FindObjectsOfType<Renderer>();
        if (renderers.Length == 0) {
            Debug.LogError("No renderers found to calculate bounds!");
            return;
        }

        // Initialize bounds
        var combinedBounds = renderers[0].bounds;
        foreach (var rend in renderers) {
            combinedBounds.Encapsulate(rend.bounds);
        }

        leftEdge = new Vector3(combinedBounds.min.x + 20f, transform.position.y + 2f, transform.position.z);
        rightEdge = new Vector3(combinedBounds.max.x - 20f, transform.position.y + 2f, transform.position.z);
    }
    
    IEnumerator PanAcrossMap() {

        LevelManager.Instance.LevelState.IsPanning = true;
        
        var elapsed = 0f;
        var originalSize = mainCamera.orthographicSize;
        var originalPosition = mainCamera.transform.position;
        
        //Set a larger size for the map overview
        var newSize = originalSize + 6f;
        mainCamera.orthographicSize = newSize;
 
        while (elapsed < StartCameraPanDuration) {
            var t = elapsed / StartCameraPanDuration;
            var camPos = Vector3.Lerp(leftEdge, rightEdge, t);
            camPos.z = originalPosition.z;
            mainCamera.transform.position = camPos;
 
            elapsed += Time.deltaTime;
            yield return null;
        }
 
        yield return new WaitForSeconds(1f);
        
        mainCamera.orthographicSize = originalSize;
        mainCamera.transform.position = originalPosition;
        
        LevelManager.Instance.LevelState.IsPanning = false;
    }
}