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
        // ��Ұ� E �����Է���
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (persistentAbilityPrefab != null && abilityNum>0)
            {
                Debug.Log("place ability");
                

               // persistentAbilityPrefab.tag = "Ability"; // ����Ϊʹ�ñ�ǩ

                GameObject newAbility = Instantiate(persistentAbilityPrefab, 
                    transform.position + transform.right * instantiateDistance, Quaternion.identity);
                
                // �ж���ҵ�ǰ�������ͣ�������Ҫ�����������
                Rigidbody2D playerRb = player.GetComponent<Rigidbody2D>();
                targetRb = newAbility.GetComponent<Rigidbody2D>();

                targetRb.gravityScale = playerRb.gravityScale; // ��������ֵ��ability

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
