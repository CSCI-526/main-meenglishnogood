using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupAndPlace : MonoBehaviour
{
    private int abilityNum = 0;
    public float instantiateDistance = 1.0f;
    public GameObject persistentAbilityPrefab;
    public GameObject player;

    private Rigidbody2D playerRb;
    private Rigidbody2D targetRb;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 玩家按 E 键尝试放置
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (persistentAbilityPrefab != null && abilityNum>0)
            {
                Debug.Log("place ability");
                // 判断玩家当前重力类型，物体需要跟随玩家重力
                Rigidbody2D playerRb = player.GetComponent<Rigidbody2D>();
                targetRb = persistentAbilityPrefab.GetComponent<Rigidbody2D>();

                targetRb.gravityScale = playerRb.gravityScale; // 将重力赋值给ability

               // persistentAbilityPrefab.tag = "Ability"; // 设置为使用标签

                Instantiate(persistentAbilityPrefab, 
                    transform.position + transform.right * instantiateDistance, Quaternion.identity);
                abilityNum--;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {   
        // collect ability
        if (other.CompareTag("Ability")) 
        {
            Debug.Log("Collide with ability");
            abilityNum++;
            Destroy(other.gameObject);
        }
    }

}
