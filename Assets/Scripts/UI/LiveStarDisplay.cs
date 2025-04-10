using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiveStarDisplay : MonoBehaviour
{
    public PowerupStarCollisionTracking tracker;
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
        if (tracker.starCount > 2) {
            rightStar.SetActive(true);
        } else if (tracker.starCount > 1) {
            midStar.SetActive(true);
        } else if (tracker.starCount > 0) {
            leftStar.SetActive(true);
        }
    }
}
