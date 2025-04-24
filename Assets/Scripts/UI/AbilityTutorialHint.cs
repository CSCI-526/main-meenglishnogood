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
        if(CountHint!=null)
        {
            CountHint.SetActive(true);
        }
        if(DescHint != null)
        {
            DescHint.SetActive(true);
        }
        if(PressToContinue != null)
        {
            PressToContinue.SetActive(true);
        }
        Time.timeScale = 0f;
        tutorialActive = true;

    }

    void ResumeGame()
    {
        if (CountHint != null )
        {
            CountHint.SetActive(false);
        }
        if(DescHint != null)
        {
           DescHint.SetActive(false);
        }
        if(PressToContinue != null)
        {
            PressToContinue.SetActive(false);
        }

        Time.timeScale = 1f;
        tutorialActive = false;
        Destroy(gameObject);


    }
}
