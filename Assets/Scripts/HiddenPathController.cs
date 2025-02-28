using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiddenPathController : MonoBehaviour
{
    private SpriteRenderer hiddenPath;
    private BoxCollider2D boxCollider;
    private bool isDay = true;

    // Start is called before the first frame update
    void Start()
    {
        hiddenPath = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();

        SetAlpha(1);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetNightMode(bool day)
    {
        isDay = day;
        if (isDay)
        {
            HidePath();
            boxCollider.isTrigger = false;
        }
        else
        {
            //ShowPath();
            boxCollider.isTrigger = true;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (isDay == false && other.CompareTag("Player"))
        {
            ShowPath();
        }
    }

    void HidePath()
    {
        Debug.Log("HidePath");
        SetAlpha(1); //visible obstacles
    }

    void ShowPath()
    {
        Debug.Log("ShowPath");
        SetAlpha(0); //invisible obstacles, which means you can see the path
    }

    void SetAlpha(float alpha)
    {
        if (hiddenPath != null)
        {
            Color color = hiddenPath.color;
            color.a = alpha;
            hiddenPath.color = color;

        }
    }
}

