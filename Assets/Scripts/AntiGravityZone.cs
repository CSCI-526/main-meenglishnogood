using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// public class AntiGravityZone : MonoBehaviour
// {
//     // Start is called before the first frame update
//     void Start()
//     {
        
//     }

//     // Update is called once per frame
//     void Update()
//     {
        
//     }
// }

// public class AntiGravityZone : MonoBehaviour
// {
//     public float antiGravityScale = -1.0f; // 反重力值（负值让玩家被吸向天花板）
//     private float originalGravityScale; // 记录玩家原始重力

//     private void OnTriggerEnter2D(Collider2D other)
//     {
//         if (other.CompareTag("Player")) // 确保只有玩家受影响
//         {
//             Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
//             if (rb != null)
//             {
//                 originalGravityScale = rb.gravityScale; // 记录原始重力
//                 rb.gravityScale = antiGravityScale; // 设置反重力
//                 Debug.Log("玩家进入反重力区域！");
//             }
//         }
//     }


//     private void OnTriggerExit2D(Collider2D other)
//     {
//         if (other.CompareTag("Player")) // 确保只有玩家受影响
//         {
//             Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
//             if (rb != null)
//             {
//                 rb.gravityScale = originalGravityScale; // 恢复原始重力
//                 Debug.Log("玩家离开反重力区域，恢复正常重力！");
//             }
//         }
//     }
// }

using UnityEngine;


public class AntiGravityZone : MonoBehaviour
{
    public float antiGravityScale = -1.0f;
    private float originalGravityScale;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();

            if (player != null)
            {
                Debug.Log("检测 isSmall 值：" + player.IsSmall());

                if (player.IsSmall()) // **只有缩小时才进入反重力**
                {
                    Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
                    if (rb != null)
                    {
                        originalGravityScale = rb.gravityScale;
                        rb.gravityScale = antiGravityScale; // 只有缩小时才改变重力
                        rb.velocity = new Vector2(rb.velocity.x, 5f); // 立即向上吸
                        Debug.Log("玩家缩小，进入反重力区域！");
                    }
                }
                else
                {
                    Debug.Log("玩家太大，无法进入反重力区域！");
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

            if (player != null && player.IsSmall()) // **只有缩小时才恢复重力**
            {
                Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    rb.gravityScale = originalGravityScale;
                    Debug.Log("玩家离开反重力区域，恢复正常重力！");
                }
            }
        }
    }
}
