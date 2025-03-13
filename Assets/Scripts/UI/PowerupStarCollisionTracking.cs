using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using TMPro;

public class PowerupStarCollisionTracking : MonoBehaviour
{
    private int consumCount = 0;
    private Color consumColor = Color.black;

    // public GameObject consumUI;
    public GameObject slotImageObject;
    //public GameObject consumTextObject;

    public TextMeshProUGUI consumText;

    private Image slotImage;

    public int starCount = 0;
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
            UpdateUI();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Change blocks to persistent consumable
        if (collision.CompareTag("Ability")) {
            if (consumColor != collision.GetComponent<Consumable>().c) {
                consumCount = 1;
                consumColor = collision.GetComponent<Consumable>().c;
            } else {
                consumCount++;
            }
            UpdateUI();
        } else if (collision.CompareTag("Star")) {
            starCount++;
        }
    }

    public void UseConsumable()
    {
        
    }

    private void UpdateUI()
    {
        if (consumCount > 0) {
            slotImage.color = consumColor;
            consumText.text = consumCount.ToString();
        } else {
            consumColor = Color.black;
            consumCount = 0;
            slotImage.color = Color.black;
            consumText.text = "";
        }
    }

}
