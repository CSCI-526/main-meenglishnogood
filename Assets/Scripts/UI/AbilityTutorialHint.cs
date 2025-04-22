using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityTutorialHint : MonoBehaviour
{

    public GameObject CountHint;
    public GameObject DescHint;
    public GameObject PressToContinue;

    private bool tutorialActive = false;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (tutorialActive && Input.anyKeyDown)
        {
            ResumeGame();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))  // player enters
        {
            ShowTutorial();
        }
    }

    void ShowTutorial()
    {
        if(CountHint!=null && DescHint != null && PressToContinue!=null)
        {
            CountHint.SetActive(true);
            DescHint.SetActive(true);
            PressToContinue.SetActive(true);
            Time.timeScale = 0f;
            tutorialActive = true;
        }

    }

    void ResumeGame()
    {
        if (CountHint != null && DescHint != null && PressToContinue!=null)
        {
            CountHint.SetActive(false);
            DescHint.SetActive(false);
            PressToContinue.SetActive(false);
            Time.timeScale = 1f;
            tutorialActive = false;
            Destroy(gameObject);
        }
                     
    }
}
