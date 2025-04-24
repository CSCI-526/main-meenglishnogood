using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityFeature : MonoBehaviour
{
    public bool ifUsed = false;
    // Start is called before the first frame update
    void Start()
    {
        // gameObject.tag = "Ability";
        // Rigidbody2D Rb = gameObject.GetComponent<Rigidbody2D>();
        // Rb.gravityScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void onTriggerEnter2D(Collider2D other)
    {
        //if (ifUsed)
        //{
        //    return;
        //}

        //// if placed on persistent block, it can be reused again
        ////if (!other.CompareTag("InvisibleWall"))
        ////{
        ////    Debug.Log("Ability Feature");
        ////    gameObject.tag = "Ability";
        ////}
        
        //if(other.CompareTag("InvisibleWall"))// placed on changeable platform
        //{
        //    ifUsed = true;
        //    Debug.Log("AbilityFeature: ifUsed = true");
        //}

            
    }
}
