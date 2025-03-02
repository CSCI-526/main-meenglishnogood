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
using UnityEngine;

public class AntiGravityZone : MonoBehaviour
{
    public float antiGravityScale = -1.0f; // 反重力值（负值让玩家被吸向天花板）
    private float originalGravityScale; // 记录玩家原始重力

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // 确保只有玩家受影响
        {
            Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                originalGravityScale = rb.gravityScale; // 记录原始重力
                rb.gravityScale = antiGravityScale; // 设置反重力
                Debug.Log("玩家进入反重力区域！");
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // 确保只有玩家受影响
        {
            Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.gravityScale = originalGravityScale; // 恢复原始重力
                Debug.Log("玩家离开反重力区域，恢复正常重力！");
            }
        }
    }
}
