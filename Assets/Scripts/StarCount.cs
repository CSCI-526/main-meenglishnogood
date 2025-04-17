using UnityEngine;

public class StarCount : MonoBehaviour
{
    public string levelName;
    public GameObject leftStar;
    public GameObject midStar;
    public GameObject rightStar;

    void Start()
    {
        int stars = PlayerPrefs.GetInt(levelName + "_Stars", 0);
        leftStar.SetActive(stars >= 1);
        midStar.SetActive(stars >= 2);
        rightStar.SetActive(stars >= 3);
    }
}
