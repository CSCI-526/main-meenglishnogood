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

using UnityEngine;

public class PlayerShrink : MonoBehaviour
{
    [Header("Shrink Settings")]
    public float shrinkFactor = 0.5f; // 缩小比例 (0.5 表示缩小到一半)
    public string triangleTag = "Triangle"; // 需要检测的三角形物体的Tag
    private bool hasShrunk = false; // 防止多次缩小

    private void OnTriggerEnter2D(Collider2D other)  // 2D 物理触发事件
    {
        if (other.CompareTag(triangleTag) && !hasShrunk)
        {
            ShrinkPlayer();
        }
    }

    private void ShrinkPlayer()
    {
        transform.localScale *= shrinkFactor; // 缩小玩家
        hasShrunk = true; // 标记已缩小，防止多次触发
        Debug.Log("Player has shrunk!"); // 输出调试信息
    }

}
