using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisibleAtNight : MonoBehaviour
{
    private SpriteRenderer hiddenPath;
    private BoxCollider2D boxCollider;
    private bool isDay = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
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
            boxCollider.isTrigger = true;
            SetAlpha(0);
        }
        else
        {
            boxCollider.isTrigger = false;
            SetAlpha(1);
        }
    }

    void SetAlpha(float alpha)
    {
        Color color = hiddenPath.color;
        color.a = alpha;
        hiddenPath.color = color;
    }
}
