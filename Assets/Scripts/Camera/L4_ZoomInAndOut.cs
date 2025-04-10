using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L4_ZoomInAndOut : MonoBehaviour
{

    public Camera mainCamera;
    public float normalSize = 4.4f;
    public float zoomOutSize = 8f;
    public float zoomSpeed = 2f;
    public float downOffset = 10f;

    private Vector3 initialOffset;

    //public Vector3 originalLocalPos;
    

    private Coroutine zoomCoroutine;

    private void Awake()
    {
        initialOffset = mainCamera.transform.localPosition;
    }

    // Start is called before the first frame update
    void Start()
    {
        //normalPosition = mainCamera.transform.position;
        //initialOffset = mainCamera.transform.localPosition;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("ZoomOutTrigger"))
        {
            Debug.Log("Zoom out");
            Vector3 offsetDown = initialOffset + new Vector3(0, -downOffset, 0);
            StartZoom(zoomOutSize, offsetDown);

        }
        else if (other.CompareTag("ZoomInTrigger"))
        {
            StartZoom(normalSize, initialOffset);
        }

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void StartZoom(float targetSize, Vector3 targetLocalPos)
    {
        if (zoomCoroutine != null)
            StopCoroutine(zoomCoroutine);
        zoomCoroutine = StartCoroutine(ZoomAndMove(targetSize, targetLocalPos));
    }

    private IEnumerator ZoomAndMove(float targetSize, Vector3 targetLocalPos)
    {
        while (Mathf.Abs(mainCamera.orthographicSize - targetSize) > 0.01f ||
               Vector3.Distance(mainCamera.transform.localPosition, targetLocalPos) > 0.01f)
        {
            mainCamera.orthographicSize = Mathf.Lerp(mainCamera.orthographicSize, targetSize, Time.deltaTime * zoomSpeed);
            mainCamera.transform.localPosition = Vector3.Lerp(mainCamera.transform.localPosition, targetLocalPos, Time.deltaTime * zoomSpeed);
            yield return null;
        }

        mainCamera.orthographicSize = targetSize;
        mainCamera.transform.localPosition = targetLocalPos;
    }
}
