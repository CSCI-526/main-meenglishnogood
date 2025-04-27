using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class PickupAndPlace : MonoBehaviour
{
    public int abilityNum = 0;
    public float instantiateDistance = 1.4f;
    public GameObject persistentAbilityPrefab;
    public GameObject player;

    //public TextMeshProUGUI powerupText;

    private Rigidbody2D playerRb;
    private Rigidbody2D targetRb;

    private Animator animator;

    //private bool hasTriggered = false;
    //public float cooldownTime = 1f;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        // player push "E" to place
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (persistentAbilityPrefab != null && abilityNum>0)
            {
                Debug.Log("PickupAndPlace: place ability");


                //GameObject newAbility = Instantiate(persistentAbilityPrefab, 
                //    transform.position + transform.right * instantiateDistance, Quaternion.identity);

                // Get the player's gravity type. Game items need to follow the player's gravity.
                playerRb = player.GetComponent<Rigidbody2D>();
                GameObject newAbility = null;
                if (playerRb.gravityScale > 0)
                {
                    newAbility = Instantiate(persistentAbilityPrefab,
                        transform.position - transform.up * instantiateDistance, Quaternion.identity);
                }
                else
                {
                    newAbility = Instantiate(persistentAbilityPrefab,
                        transform.position + transform.up * instantiateDistance, Quaternion.identity);
                }


                targetRb = newAbility.GetComponent<Rigidbody2D>();
                targetRb.gravityScale = playerRb.gravityScale; // Give gravity value to game items

                //// get the current gravity of player, the powerup should have same gravity
                //playerRb = player.GetComponent<Rigidbody2D>();
                //targetRb = newAbility.GetComponent<Rigidbody2D>();

                //targetRb.gravityScale = playerRb.gravityScale; 


                abilityNum--;
                // UpdatePowerupUI();
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        
        // collect ability
        if (other.CompareTag("Ability") || other.CompareTag("Ability_Use")) 
        {
            
            bool ifUsed = other.gameObject.GetComponent<AbilityFeature>().ifUsed;
            Debug.Log("PickupAndPlace; collide with ability ifUsed = " + ifUsed);
            if (ifUsed) // if the ability is not used, then can be picked up by player
            {
                
                Debug.Log("PickupAndPlace: ability used, cannot pickup again");
                return;
            }

            Debug.Log("PickupAndPlace: Player collides with ability");
            abilityNum++;
            // UpdatePowerupUI();
            //Destroy(other.gameObject);
            other.gameObject.SetActive(false);
            //StartCoroutine(ResetTrigger());

            animator.SetTrigger("GetAbility");
        }
    }

    //IEnumerator ResetTrigger()
    //{
    //    yield return new WaitForSeconds(cooldownTime);
    //    hasTriggered = false;
    //}


    //public void UpdatePowerupUI()
    //{
    //    if (powerupText != null)
    //    {
    //        powerupText.text = "Platform changing item: " + abilityNum;
    //    }
    //}

}
