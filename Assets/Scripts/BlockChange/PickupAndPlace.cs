using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class PickupAndPlace : MonoBehaviour
{
    private int abilityNum = 0;
    public float instantiateDistance = 1.0f;
    public GameObject persistentAbilityPrefab;
    public GameObject player;

    public TextMeshProUGUI powerupText;

    private Rigidbody2D playerRb;
    private Rigidbody2D targetRb;

    // Start is called before the first frame update
    void Start()
    {
        UpdatePowerupUI();
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
                

               // persistentAbilityPrefab.tag = "Ability"; // 设置为使用标签

                GameObject newAbility = Instantiate(persistentAbilityPrefab, 
                    transform.position + transform.right * instantiateDistance, Quaternion.identity);
                
                // 判断玩家当前重力类型，物体需要跟随玩家重力
                Rigidbody2D playerRb = player.GetComponent<Rigidbody2D>();
                targetRb = newAbility.GetComponent<Rigidbody2D>();

                targetRb.gravityScale = playerRb.gravityScale; // 将重力赋值给ability

                abilityNum--;
                UpdatePowerupUI();
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
            UpdatePowerupUI();
            Destroy(other.gameObject);
        }
    }

    void UpdatePowerupUI()
    {
        if (powerupText != null)
        {
            powerupText.text = "Platform changing item: " + abilityNum;
        }
    }

}
