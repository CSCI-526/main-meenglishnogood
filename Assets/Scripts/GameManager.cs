using System;
using UnityEngine;
using static Constants;
using System.Collections;
using System.Collections.Generic;
using static TimeOfDay;

public class GameManager : MonoBehaviour  {
    
    public static GameManager Instance { get; private set; }
    public GameState GameState { get; private set; }
    public PlayerState PlayerState { get; private set; }
    public Camera mainCamera;
    public SpriteRenderer sunSprite;
    
    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
            return;
        }
        
        Instance = this;
        DontDestroyOnLoad(gameObject);
        
        GameState = new GameState(mainCamera, sunSprite);
        var dayNightMutableObjects = new List<GameObject>();
        foreach (var component in FindObjectsOfType<MonoBehaviour>(true)) {
            if (component is IDayNightMutable) {
                dayNightMutableObjects.Add(component.gameObject);
            }
        }
        GameState.SetDayNightMutableObjects(dayNightMutableObjects);
        PlayerState = new PlayerState();
    }

    void Start() {
        InitDayTime();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Q)) {
            ToggleDayNight();
        }
    }
    
    void ToggleDayNight() {
        if (GameState.TimeOfDay == ECLIPSE) return;
        var newDayState = (GameState.TimeOfDay == DAY) ? NIGHT : DAY;
        StartCoroutine(ChangeColor(newDayState == DAY ? DayColor : NightColor));
        StartCoroutine(ChangeBackgroundColor(newDayState == DAY ? DayBackgroundColor : NightBackgroundColor));
        var dayNightMutableObjects = GameState.GetDayNightMutableObjects();
        foreach (var obj in dayNightMutableObjects) {
            var component = obj.GetComponent<IDayNightMutable>();
            obj.SetActive(component.IsVisible(newDayState));
        }
        GameState.TimeOfDay = newDayState;
    }

    IEnumerator ChangeColor(Color targetColor) {
        GameState.TimeOfDay = ECLIPSE;
        var startColor = GameState.SunSprite.color;
        var elapsedTime = 0f;

        while (elapsedTime < TransitionDuration) {
            elapsedTime += Time.deltaTime;
            GameState.SunSprite.color = Color.Lerp(startColor, targetColor, elapsedTime / TransitionDuration);
            yield return null;
        }
        GameState.SunSprite.color = targetColor;
    }

    IEnumerator ChangeBackgroundColor(Color targetColor) {
        
        var elapsedTime = 0f;
        var startColor = GameState.MainCamera.backgroundColor;

        while (elapsedTime < TransitionDuration) {
            elapsedTime += Time.deltaTime;
            GameState.MainCamera.backgroundColor = Color.Lerp(startColor, targetColor, elapsedTime / TransitionDuration);
            yield return null;
        }

        GameState.MainCamera.backgroundColor = targetColor;
    }

    private void InitDayTime() {
        GameState.SunSprite.color = DayColor;
        GameState.MainCamera.backgroundColor = DayBackgroundColor;
        foreach (var obj in GameState.GetDayNightMutableObjects()) {
            var visibility = obj.GetComponent<IDayNightMutable>();
            if (visibility != null && visibility.IsVisible(GameState.TimeOfDay)) {
                obj.SetActive(true);
            } else {
                obj.SetActive(false);
            }
        }
    }
}