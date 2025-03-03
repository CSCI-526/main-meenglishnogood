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
            position = transform.position; // 获得原来block的大小和位置
            scale = transform.localScale;

            // 销毁原来的block
            Destroy(gameObject);

            // 销毁使用过的ability
            Destroy(other.gameObject);

            // 创建新的persistentblock
            GameObject newBlock = Instantiate(persistentBlockPrefab, position, Quaternion.identity);
            newBlock.transform.localScale = scale;


        }
    }
}
