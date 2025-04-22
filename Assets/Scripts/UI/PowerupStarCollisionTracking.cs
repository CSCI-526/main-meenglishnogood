using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using UnityEngine.SceneManagement;
using TMPro;

public class PowerupStarCollisionTracking : MonoBehaviour
{
    private int consumCount = 0;
    private Color consumColor = Color.black;
    public Color abilityColor;

    // public GameObject consumUI;
    public GameObject slotImageObject;
    //public GameObject consumTextObject;

    public TextMeshProUGUI consumText;

    private Image slotImage;

    public int starCount = 0;

    public AnalyticsManager db;

    // Start is called before the first frame update
    void Start()
    {
        slotImage =  slotImageObject.GetComponent<Image>();
        slotImage.color = Color.black;

        //consumText = consumText.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) {
            if (consumCount > 0) {
            consumCount--;
            db.AddCollectibleData("Use " + consumColor.ToString(), SceneManager.GetActiveScene().name);
            UpdateUI();
            }
        } else if (Input.GetKeyDown(KeyCode.Q)) {
            db.AddCollectibleData("Q", SceneManager.GetActiveScene().name);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Change blocks to persistent consumable
        if (collision.CompareTag("Ability")) {
            bool ifUsed = collision.gameObject.GetComponent<AbilityFeature>().ifUsed;
            Debug.Log("PowerupStarTracking; collide with ability ifUsed = " + ifUsed);
            if (ifUsed) return;

            if (consumColor != collision.GetComponent<Consumable>().c) {
                consumCount = 1;
                consumColor = collision.GetComponent<Consumable>().c;
            } else {
                consumCount++;
            }
            db.AddCollectibleData("Get " + consumColor.ToString(), SceneManager.GetActiveScene().name);
            UpdateUI();
        } else if (collision.CompareTag("Star")) {
            starCount++;
            db.AddCollectibleData("Star", SceneManager.GetActiveScene().name);
        } else if (collision.CompareTag("ShrinkTriangle")) {
            db.AddCollectibleData("ShrinkTriangle", SceneManager.GetActiveScene().name);
        } else if (collision.CompareTag("GrowTriangle")) {
            db.AddCollectibleData("GrowTriangle", SceneManager.GetActiveScene().name);
        } else if (collision.CompareTag("Portal")) {
            // Debug.Log("Portal logged");
            db.AddCollectibleData("Portal", SceneManager.GetActiveScene().name);
        }
    }

    private void UpdateUI()
    {
        if (consumCount > 0) {
            slotImage.color = abilityColor;
            consumText.text = consumCount.ToString();
            Debug.Log("PowerupTracking: >0  - text and color updated");
        } else {
            consumColor = Color.black;
            consumCount = 0;
            slotImage.color = Color.black;
            consumText.text = "";
        }
    }

    public void SetConsumCountAndUpdateUI(int count)
    {
        
        consumCount = count;
        Debug.Log("PowerupTracking: SetConsumCountAndUpdateUI called - consumCount: " + consumCount);
        UpdateUI();
    }

}
