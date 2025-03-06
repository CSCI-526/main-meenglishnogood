using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalController : MonoBehaviour
{

    public GameObject endUI;
    public PowerupStarCollisionTracking starTracking;
    public GameObject leftStar;
    public GameObject midStar;
    public GameObject rightStar;
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
            if (starTracking.starCount > 0) {
                leftStar.SetActive(true);
            }
            if (starTracking.starCount > 1) {
                midStar.SetActive(true);
            }
            if (starTracking.starCount > 2) {
                rightStar.SetActive(true);
            }

        }
    }
}
