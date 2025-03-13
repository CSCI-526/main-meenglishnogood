using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoalController : MonoBehaviour
{

    public GameObject endUI;
    public GameObject menuUI;
    public PowerupStarCollisionTracking starTracking;
    public GameObject leftStar;
    public GameObject midStar;
    public GameObject rightStar;
    //public Image leftStar;
    //public Image midStar;
    //public Image rightStar;

    public GameObject grayOut;
    public GameObject settingsButton;
    // 
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
        if (other.CompareTag("Player"))
        {

            Destroy(gameObject);

            Time.timeScale = 0f;
            endUI.SetActive(true);
            grayOut.SetActive(true);
            if (starTracking.starCount > 0) {
                leftStar.SetActive(true);
            }
            if (starTracking.starCount > 1) {
                midStar.SetActive(true);
            }
            if (starTracking.starCount > 2) {
                rightStar.SetActive(true);
            }
            menuUI.SetActive(true);
            settingsButton.SetActive(false);
        }
    }
}
