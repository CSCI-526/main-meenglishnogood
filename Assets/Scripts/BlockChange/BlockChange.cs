using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockChange : MonoBehaviour
{
    public GameObject persistentBlockPrefab;

    Vector3 position;
    Vector3 scale;

    SpriteRenderer sourceRenderer;
    SpriteRenderer newRenderer;

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

            sourceRenderer = gameObject.GetComponent<SpriteRenderer>();
            

            //position = transform.position; // get the original size and position of blocks
            //scale = transform.localScale;

            // Make original block inactive
            // Destroy(gameObject);
            gameObject.SetActive(false);
            // destroy the used ability
            Destroy(other.gameObject);

            // create new persistentblock
            position = transform.position; // get the position of the original block
            //scale = transform.localScale;
            GameObject newBlock = Instantiate(persistentBlockPrefab, position, Quaternion.identity);

            newRenderer = newBlock.GetComponent<SpriteRenderer>();

            // Copy the tile size 
            newRenderer.size = sourceRenderer.size;

            // make sure the scale is 1
            newBlock.transform.localScale = gameObject.transform.localScale;


        }
    }
}
