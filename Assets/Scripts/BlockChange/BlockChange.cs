using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockChange : MonoBehaviour
{
    public GameObject persistentBlockPrefab;

    Vector3 position;
    Vector3 scale;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ability") && gameObject.CompareTag("InvisibleWall"))
        {
            position = transform.position; // ���ԭ��block�Ĵ�С��λ��
            scale = transform.localScale;

            // ����ԭ����block
            Destroy(gameObject);

            // ����ʹ�ù���ability
            Destroy(other.gameObject);

            // �����µ�persistentblock
            GameObject newBlock = Instantiate(persistentBlockPrefab, position, Quaternion.identity);
            newBlock.transform.localScale = scale;


        }
    }
}
