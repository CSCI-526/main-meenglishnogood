using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderController : MonoBehaviour
{
    private int climbHeight = 8;
    private int climbSpeed = 3;
    private float startY;
    private bool isDay = true;
    private BoxCollider2D boxCollider;

    // Start is called before the first frame update
    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (isDay == false && other.CompareTag("Player"))
        {
            Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
            startY = other.transform.position.y;

            if (other.transform.position.y < startY + climbHeight)
            {
                rb.velocity = new Vector2(rb.velocity.x, climbSpeed);
            }
            else
            {
                //stop going up
                rb.velocity = new Vector2(rb.velocity.x, 0);
            }

        }
    }

    public void SetNightMode(bool day)
    {
        isDay = day;
        if (isDay)
        {
            //cannot use ladder at day
            boxCollider.isTrigger = false;
        }
        else
        {
            boxCollider.isTrigger = true;
        }
    }
}
