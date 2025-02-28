using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisibleAtDay : MonoBehaviour
{
    private SpriteRenderer hiddenPath;
    private BoxCollider2D boxCollider;
    private bool isDay = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        hiddenPath = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
        SetAlpha(0);
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
            boxCollider.isTrigger = false;
            SetAlpha(1);
        }
        else
        {
            boxCollider.isTrigger = true;
            SetAlpha(0);
        }
    }

    void SetAlpha(float alpha)
    {
        Color color = hiddenPath.color;
        color.a = alpha;
        hiddenPath.color = color;
    }
}
