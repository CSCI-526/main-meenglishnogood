using System.Collections;
using UnityEngine;
using static Constants;

[RequireComponent(typeof(Camera))]
public class MainCameraController : MonoBehaviour
{
    private Camera mainCamera;
    private Vector3 leftEdge;
    private Vector3 rightEdge;

    private void Start()
    {
        InitializeCamera();
        SetCameraBoundsFromScene();
        StartCoroutine(PanAcrossMapRoutine());
    }

    private void InitializeCamera()
    {
        mainCamera = GetComponent<Camera>();
        LevelManager.Instance.mainCamera = mainCamera;
    }

    private void SetCameraBoundsFromScene()
    {
        var renderers = FindObjectsOfType<Renderer>();
        if (renderers.Length == 0)
        {
            Debug.LogError("No renderers found in scene to calculate camera bounds.");
            return;
        }

        var bounds = renderers[0].bounds;
        foreach (var rend in renderers)
        {
            bounds.Encapsulate(rend.bounds);
        }

        float edgeOffset = 20f;
        float heightOffset = 2f;
        float y = transform.position.y + heightOffset;
        float z = transform.position.z;

        leftEdge = new Vector3(bounds.min.x + edgeOffset, y, z);
        rightEdge = new Vector3(bounds.max.x - edgeOffset, y, z);
    }

    private IEnumerator PanAcrossMapRoutine()
    {
        var levelState = LevelManager.Instance.LevelState;
        levelState.IsPanning = true;

        float elapsed = 0f;
        float originalSize = mainCamera.orthographicSize;
        Vector3 originalPosition = mainCamera.transform.position;

        float zoomedOutSize = originalSize + 6f;
        mainCamera.orthographicSize = zoomedOutSize;

        while (elapsed < StartCameraPanDuration)
        {
            float t = elapsed / StartCameraPanDuration;
            mainCamera.transform.position = Vector3.Lerp(leftEdge, rightEdge, t);
            elapsed += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(1f);

        mainCamera.orthographicSize = originalSize;
        mainCamera.transform.position = originalPosition;

        levelState.IsPanning = false;
    }
}
