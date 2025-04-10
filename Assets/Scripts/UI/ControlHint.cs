using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlHint : MonoBehaviour
{
    public GameObject ControlHintObject;  
    public float duration = 5f;
    public float shakeAmount = 3f;
    public float shakeSpeed = 1f;
    private Vector3 originalPos;
    private Vector3 shakeDirection;
    private RectTransform rectTransform;

    public Button buttonToMonitor;

    // Start is called before the first frame update
    void Start()
    {
        rectTransform = ControlHintObject.gameObject.GetComponent<RectTransform>();
        originalPos = rectTransform.localPosition;  // initial position
        shakeDirection = new Vector3(1, 1, 0).normalized;
        DeactivateSelf();
        StartCoroutine(ShakeThenHide()); 
    }

    // Update is called once per frame
    void Update()
    {
        if(buttonToMonitor != null)
        {
            buttonToMonitor.onClick.AddListener(DeactivateSelf);
        }
        
    }


    System.Collections.IEnumerator ShakeThenHide()
    {

        // wait for the camera overview
        yield return new WaitForSeconds(3f);

        ControlHintObject.SetActive(true);

        float timer = 0f;

        while (timer < duration)
        {
            float shakeOffset = Mathf.Sin(timer * shakeSpeed) * shakeAmount;
            rectTransform.localPosition = originalPos + shakeDirection * shakeOffset;

            timer += Time.deltaTime;
            yield return null;
        }

        DeactivateSelf(); 
    }

    void DeactivateSelf()
    {
        // 将自身设为inactive
        ControlHintObject.gameObject.SetActive(false);
    }

}
