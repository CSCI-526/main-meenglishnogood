using System;
using System.Collections;
//using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class DayNightController : MonoBehaviour
{
    public SpriteRenderer sunSprite;  // Assign your Sun's SpriteRenderer in Inspector
    public Color dayColor = Color.yellow;
    public Color nightColor = Color.black; // Dark navy blue for night
    public Color dayBackgroundColor = new Color(0.5f, 0.8f, 1.0f); // Light blue sky
    public Color nightBackgroundColor = new Color(0.05f, 0.05f, 0.2f); // Dark night sky
    public float transitionDuration = 1.5f; // Time for smooth transition

    public bool isDay = true;
    //public PlayerHealth playerHealth;
    public Camera mainCamera;

    public bool isTransitioning = false;

    void Start()
    {
        sunSprite.color = isDay ? dayColor : nightColor;
        mainCamera.backgroundColor = isDay ? dayBackgroundColor : nightBackgroundColor;
        UpdateInvisibleWallsState();
        UpdatePortalState();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && !isTransitioning) // Detect if "Q" key is pressed
        {
            ToggleDayNight();
        }
    }
    void ToggleDayNight()
    {
        if (isTransitioning) return;
        isDay = !isDay;
        StartCoroutine(ChangeColor(isDay ? dayColor : nightColor));
        StartCoroutine(ChangeBackgroundColor(isDay ? dayBackgroundColor : nightBackgroundColor));
        //StartCoroutine(ChanagePlatformVisibility(dayBackgroundColor, nightBackgroundColor));

        // Inform PlayerHealth about Day/Night Change
        //playerHealth.setNightTime(!isDay);

        // Update Hidden Paths Visibility Based on Time
        PortalController[] portals = FindObjectsByType<PortalController>(FindObjectsSortMode.None);
        foreach (PortalController portal in portals)
        {
            if (portal.otherPortal != null)
            {

                portal.TryTeleport();
            }
        }

        //HiddenPathController[] hiddenPaths = FindObjectsByType<HiddenPathController>(FindObjectsSortMode.None);
        //foreach (HiddenPathController path in hiddenPaths)
        //{
        //    path.SetNightMode(isDay);
        //}

        //LadderController[] climbablePlatforms = FindObjectsByType<LadderController>(FindObjectsSortMode.None);
        //foreach (LadderController platform in climbablePlatforms)
        //{
        //    platform.SetNightMode(isDay);
        //}

        //VisibleAtDay[] visibleObstacles = FindObjectsByType<VisibleAtDay>(FindObjectsSortMode.None);
        //foreach (VisibleAtDay obs in visibleObstacles)
        //{
        //    obs.SetNightMode(isDay);
        //}

        //VisibleAtNight[] invisibleObstacles = FindObjectsByType<VisibleAtNight>(FindObjectsSortMode.None);
        //foreach (VisibleAtNight obs in invisibleObstacles)
        //{
        //    obs.SetNightMode(isDay);
        //}

    }

    IEnumerator ChangeColor(Color targetColor)
    {
        isTransitioning = true;
        InvisibleWalls[] invisibleWalls = FindObjectsOfType<InvisibleWalls>();
        PortalController[] portals = FindObjectsOfType<PortalController>();

        Color startColor = sunSprite.color;
        float elapsedTime = 0f;
        if (!isDay)  // at night
        {
            foreach (InvisibleWalls wall in invisibleWalls)
            {
                if (wall.mode == 2)
                {
                    wall.GetComponent<BoxCollider2D>().enabled = !isDay;
                }
                else if (wall.mode == 3)
                {
                    wall.spriteRenderer.color = !isDay ? dayBackgroundColor : nightBackgroundColor;
                }
            }
            foreach (PortalController portal in portals)
            {
                if (portal.mode == 2)
                {
                    portal.GetComponent<BoxCollider2D>().enabled = !isDay;
                }
            }
        }
        else if (isDay)
        {
            foreach (InvisibleWalls wall in invisibleWalls)
            {
                if (wall.mode == 1)
                {
                    wall.GetComponent<BoxCollider2D>().enabled = isDay;
                }
                else if (wall.mode == 3)
                {
                    wall.spriteRenderer.color = !isDay ? dayBackgroundColor : nightBackgroundColor;
                }
            }
            foreach (PortalController portal in portals)
            {
                if (portal.mode == 1)
                {
                    portal.GetComponent<BoxCollider2D>().enabled = isDay;
                }
            }
        }

        while (elapsedTime < transitionDuration)
        {
            elapsedTime += Time.deltaTime;
            sunSprite.color = Color.Lerp(startColor, targetColor, elapsedTime / transitionDuration);
            yield return null;
        }
        sunSprite.color = targetColor;

        if (!isDay)
        {
            foreach (InvisibleWalls wall in invisibleWalls)
            {
                if (wall != null && wall.mode == 1)
                {
                    wall.GetComponent<BoxCollider2D>().enabled = isDay;
                }

            }
            foreach (PortalController portal in portals)
            {
                if (portal.mode == 1)
                {
                    portal.GetComponent<BoxCollider2D>().enabled = isDay;
                }
            }
        }
        else if (isDay)
        {
            foreach (InvisibleWalls wall in invisibleWalls)
            {
                if (wall.mode == 2)
                {
                    wall.GetComponent<BoxCollider2D>().enabled = !isDay;
                }
            }
            foreach (PortalController portal in portals)
            {
                if (portal.mode == 2)
                {
                    portal.GetComponent<BoxCollider2D>().enabled = !isDay;
                }
            }
        }
        isTransitioning = false;
    }

    IEnumerator ChangeBackgroundColor(Color targetColor)
    {
        Color startColor = mainCamera.backgroundColor;
        float elapsedTime = 0f;

        while (elapsedTime < transitionDuration)
        {
            elapsedTime += Time.deltaTime;
            mainCamera.backgroundColor = Color.Lerp(startColor, targetColor, elapsedTime / transitionDuration);
            yield return null;
        }

        mainCamera.backgroundColor = targetColor;
    }

    // too slow to do
    //IEnumerator ChanagePlatformVisibility(Color transparentColor, Color nonTransColor)
    //{
    //    InvisibleWalls[] invisibleWalls = FindObjectsOfType<InvisibleWalls>();
    //    foreach (InvisibleWalls wall in invisibleWalls)
    //    {
    //        SpriteRenderer renderer = wall.GetComponent<SpriteRenderer>();
    //        Color startColor = renderer.color;
    //        float elapsedTime = 0f;
    //        if(startColor != transparentColor) // if the object is not transparent, make it transparent
    //        {
    //            while (elapsedTime < transitionDuration)
    //            {
    //                elapsedTime += Time.deltaTime;
    //                renderer.color = Color.Lerp(startColor, transparentColor, elapsedTime / transitionDuration);
    //                yield return null;
    //            }
    //            renderer.color = transparentColor;
    //        }
    //        else // if the object already transparent, make it visible
    //        {
    //            while (elapsedTime < transitionDuration)
    //            {
    //                elapsedTime += Time.deltaTime;
    //                renderer.color = Color.Lerp(startColor, nonTransColor, elapsedTime / transitionDuration);
    //                yield return null;
    //            }
    //            renderer.color = nonTransColor;
    //        }
          
            
    //    }
        
    //}

    void UpdateInvisibleWallsState()
    {

        InvisibleWalls[] invisibleWalls = FindObjectsOfType<InvisibleWalls>();
        foreach (InvisibleWalls wall in invisibleWalls)
        {
            if (wall.mode == 1) 
            {
                wall.GetComponent<BoxCollider2D>().enabled = isDay;
            }
            else if (wall.mode == 2) 
            {
                wall.GetComponent<BoxCollider2D>().enabled = !isDay;  
            }
        }
    }
    void UpdatePortalState()
    {

        PortalController[] portals = FindObjectsByType<PortalController>(FindObjectsSortMode.None);
        foreach (PortalController portal in portals)
        {
            if (portal.mode == 1)
            {
                portal.GetComponent<BoxCollider2D>().enabled = isDay;
            }
            else if (portal.mode == 2)
            {
                portal.GetComponent<BoxCollider2D>().enabled = !isDay;
            }
        }
    }
}