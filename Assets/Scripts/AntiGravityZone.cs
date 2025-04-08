using UnityEngine;


public class AntiGravityZone : MonoBehaviour
{
    public float antiGravityScale = -1.0f;
    private float originalGravityScale = 1f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();

            if (player != null)
            {
                Debug.Log("isSmall：" + player.IsSmall());

                if (player.IsSmall()) // The player can enter anti-gravity when it is small
                {
                    Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
                    if (rb != null)
                    {
                        //originalGravityScale = rb.gravityScale;
                        //Debug.Log("Original Gravity Scale: " + originalGravityScale);
                        rb.gravityScale = antiGravityScale; 
                        rb.velocity = new Vector2(rb.velocity.x, 5f); 
                        Debug.Log("The player is small. Enter anti-gravity zone.");
                    }
                }
                else
                {
                    Debug.Log("The player is too big to enter anti-gravity zone.");
                }
            }
        }
    }


    // private void OnTriggerEnter2D(Collider2D other)
    // {
    //     if (other.CompareTag("Player"))
    //     {
    //         PlayerController player = other.GetComponent<PlayerController>();

    //         if (player != null)
    //         {
    //             Debug.Log("检测 isSmall 值：" + player.IsSmall());

    //             if (player.IsSmall()) // **只有缩小时才进入反重力**
    //             {
    //                 Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
    //                 if (rb != null)
    //                 {
    //                     originalGravityScale = rb.gravityScale;
    //                     rb.gravityScale = antiGravityScale;
    //                     rb.velocity = new Vector2(rb.velocity.x, 5f);
    //                     Debug.Log("玩家缩小，进入反重力区域！");
    //                 }
    //             }
    //             else
    //             {
    //                 Debug.Log("玩家太大，无法进入反重力区域！");
    //             }
    //         }
    //     }
    // }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();

            if (player != null && player.IsSmall())
            {
                Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    rb.gravityScale = originalGravityScale;
                    Debug.Log("The player leaves anti-gravity zone.");
                }
            }
        }
    }
}
