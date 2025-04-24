using UnityEngine;

public class PlayerSizeControll2D : MonoBehaviour
{
    [Header("Size Settings")]
    public float shrinkFactor = 0.5f;
    public float growFactor = 2.0f;
    public string shrinkTag = "ShrinkTriangle";
    public string growTag = "GrowTriangle";

    private Vector3 originalSize;
    public bool hasShrunk = false;
    private PlayerController playerController;

    private void Start()
    {
        originalSize = transform.localScale;
        playerController = GetComponent<PlayerController>();

        if (playerController == null)
        {
            Debug.LogError("No PlayerController!");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("OnTriggerEnter2D is using: " + other.gameObject.name);

        if (other.CompareTag(shrinkTag) && !hasShrunk)
        {
            ShrinkPlayer();
            //Destroy(other.gameObject);
            other.gameObject.SetActive(false);
        }
        else if (other.CompareTag(growTag) && hasShrunk)
        {
            GrowPlayer();
            //Destroy(other.gameObject);
            other.gameObject.SetActive(false);
        }
    }

    private void ShrinkPlayer()
    {
        transform.localScale *= shrinkFactor;
        hasShrunk = true;
        if (playerController != null)
        {
            playerController.SetSmallState(true);
        }
        Debug.Log("Player has shrinked！");
    }

    // private void GrowPlayer()
    // {
    //     if (transform.localScale.x < originalSize.x)
    //     {
    //         transform.localScale *= growFactor;
    //         hasShrunk = false;
    //         if (playerController != null)
    //         {
    //             playerController.SetSmallState(false);
    //         }
    //         Debug.Log("玩家已变大！");
    //     }
    //     else
    //     {
    //         Debug.Log("玩家已是原始大小，不能再变大！");
    //     }
    // }
    private void GrowPlayer()
    {
        if (transform.localScale.x < originalSize.x)
        {
            transform.localScale *= growFactor;
            hasShrunk = false;
            
            if (playerController != null)
            {
                playerController.SetSmallState(false);
                playerController.ResetGravity(); 
                Debug.Log("GrowPlayer() is using! isSmall: " + playerController.IsSmall());
            }
        }
    }
    

}
