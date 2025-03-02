using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// public class NewBehaviourScript : MonoBehaviour
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
// using UnityEngine;

// using UnityEngine;

// public class PlayerShrink : MonoBehaviour
// {
//     [Header("Shrink Settings")]
//     public float shrinkFactor = 0.5f; // 缩小比例 (0.5 表示缩小到一半)
//     public string triangleTag = "Triangle"; // 需要检测的三角形物体的Tag
//     private bool hasShrunk = false; // 防止多次缩小

//     private void OnTriggerEnter2D(Collider2D other)  // 2D 物理触发事件
//     {
//         if (other.CompareTag(triangleTag) && !hasShrunk)
//         {
//             ShrinkPlayer();
//         }
//     }

//     private void ShrinkPlayer()
//     {
//         transform.localScale *= shrinkFactor; // 缩小玩家
//         hasShrunk = true; // 标记已缩小，防止多次触发
//         Debug.Log("Player has shrunk!"); // 输出调试信息
//     }

// }
// using UnityEngine;

// public class PlayerSizeControl2D : MonoBehaviour
// {
//     [Header("Size Settings")]
//     public float shrinkFactor = 0.5f; // 缩小比例 (0.5 表示缩小到一半)
//     public float growFactor = 2.0f;   // 变大比例 (2.0 表示变大为原来的2倍)
//     public string shrinkTag = "ShrinkTriangle"; // 让玩家变小的三角形的Tag
//     public string growTag = "GrowTriangle"; // 让玩家变大的三角形的Tag
//     private bool hasShrunk = false; // 防止多次缩小
//     private bool hasGrown = false; // 防止多次变大

//     private void OnTriggerEnter2D(Collider2D other)
//     {
//         Debug.Log("触发器检测到: " + other.gameObject.name); // 调试信息

//         // 碰到缩小三角形
//         if (other.CompareTag(shrinkTag) && !hasShrunk)
//         {
//             ShrinkPlayer();
//         }
//         // 碰到变大三角形
//         else if (other.CompareTag(growTag) && !hasGrown)
//         {
//             GrowPlayer();
//         }
//     }

//     private void ShrinkPlayer()
//     {
//         transform.localScale *= shrinkFactor; // 缩小玩家
//         hasShrunk = true; // 防止重复缩小
//         hasGrown = false; // 允许再变大
//         Debug.Log("玩家已缩小！");
//     }

//     private void GrowPlayer()
//     {
//         transform.localScale *= growFactor; // 变大玩家
//         hasGrown = true; // 防止重复变大
//         hasShrunk = false; // 允许再缩小
//         Debug.Log("玩家已变大！");
//     }
// }
// using UnityEngine;

// public class PlayerSizeControl2D : MonoBehaviour
// {
//     [Header("Size Settings")]
//     public float shrinkFactor = 0.5f; // 缩小比例 (0.5 表示缩小到一半)
//     public float growFactor = 2.0f;   // 变大比例 (2.0 表示变大为原来的2倍)
//     public string shrinkTag = "ShrinkTriangle"; // 让玩家变小的三角形的Tag
//     public string growTag = "GrowTriangle"; // 让玩家变大的三角形的Tag

//     private Vector3 originalSize; // 存储玩家原始大小
//     private bool hasShrunk = false; // 是否已经变小

//     private void Start()
//     {
//         originalSize = transform.localScale; // 记录玩家的初始大小
//     }

//     private void OnTriggerEnter2D(Collider2D other)
//     {
//         Debug.Log("触发器检测到: " + other.gameObject.name); // 调试信息

//         // 碰到缩小三角形
//         if (other.CompareTag(shrinkTag) && !hasShrunk)
//         {
//             ShrinkPlayer();
//         }
//         // 碰到变大三角形，但只有在玩家变小的情况下才可以变大
//         else if (other.CompareTag(growTag) && hasShrunk)
//         {
//             GrowPlayer();
//         }
//     }

//     private void ShrinkPlayer()
//     {
//         transform.localScale *= shrinkFactor; // 缩小玩家
//         hasShrunk = true; // 记录已缩小状态
//         Debug.Log("玩家已缩小！");
//     }

//     private void GrowPlayer()
//     {
//         // 只有当玩家当前大小小于原始大小时，才允许变大
//         if (transform.localScale.x < originalSize.x)
//         {
//             transform.localScale *= growFactor; // 变大玩家
//             hasShrunk = false; // 恢复为未缩小状态
//             Debug.Log("玩家已变大！");
//         }
//         else
//         {
//             Debug.Log("玩家已是原始大小，不能再变大！");
//         }
//     }
// }
using UnityEngine;

public class PlayerSizeControl2D : MonoBehaviour
{
    [Header("Size Settings")]
    public float shrinkFactor = 0.5f; // 缩小比例 (0.5 表示缩小到一半)
    public float growFactor = 2.0f;   // 变大比例 (2.0 表示变大为原来的2倍)
    public string shrinkTag = "ShrinkTriangle"; // 让玩家变小的三角形的Tag
    public string growTag = "GrowTriangle"; // 让玩家变大的三角形的Tag

    private Vector3 originalSize; // 存储玩家原始大小
    private bool hasShrunk = false; // 是否已经变小

    private void Start()
    {
        originalSize = transform.localScale; // 记录玩家的初始大小
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("触发器检测到: " + other.gameObject.name); // 调试信息

        // 碰到缩小三角形
        if (other.CompareTag(shrinkTag) && !hasShrunk)
        {
            ShrinkPlayer();
            Destroy(other.gameObject); // 吃掉三角形（删除它）
        }
        // 碰到变大三角形，但只有在玩家变小的情况下才可以变大
        else if (other.CompareTag(growTag) && hasShrunk)
        {
            GrowPlayer();
            Destroy(other.gameObject); // 吃掉三角形（删除它）
        }
    }

    private void ShrinkPlayer()
    {
        transform.localScale *= shrinkFactor; // 缩小玩家
        hasShrunk = true; // 记录已缩小状态
        Debug.Log("玩家已缩小！");
    }

    private void GrowPlayer()
    {
        // 只有当玩家当前大小小于原始大小时，才允许变大
        if (transform.localScale.x < originalSize.x)
        {
            transform.localScale *= growFactor; // 变大玩家
            hasShrunk = false; // 恢复为未缩小状态
            Debug.Log("玩家已变大！");
        }
        else
        {
            Debug.Log("玩家已是原始大小，不能再变大！");
        }
    }
}
