using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityFeature : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // gameObject.tag = "Ability";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void onTriggerEnter2D(Collider2D other)
    {
         // if placed on persistent block, it can be reused again
         if (!other.CompareTag("Invisible"))
         {
            gameObject.tag = "Ability";
         }
    }
}
