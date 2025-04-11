using System;
using UnityEngine;
using static Constants;
using static TimeOfDay;
using System.Collections;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour  {
    
    public static LevelManager Instance { get; private set; }
    public LevelState LevelState { get; private set; }
    public PlayerState PlayerState { get; private set; }
    public Camera mainCamera;
    public SpriteRenderer sunSprite;
    
    private void Awake() {
        Instance = this;
        InitLevelState();
        InitPlayerState();
    }

    private void OnEnable() {
        InputHandler.ToggleDayNightPressed += HandleToggleDayNight;
    }
    
    private void OnDisable() {
        InputHandler.ToggleDayNightPressed -= HandleToggleDayNight;
    }
    
    private void HandleToggleDayNight() {
        if (LevelState.TimeOfDay == ECLIPSE) return;
        StartCoroutine(ToggleDayNight());
    }
    
    IEnumerator ToggleDayNight() {
        var newTimeOfDay = (LevelState.TimeOfDay == DAY) ? NIGHT : DAY;
        LevelState.TimeOfDay = ECLIPSE;
        
        var dayNightMutableObjects = LevelState.GetDayNightMutableObjects();
        ActivateVisibleMutables(newTimeOfDay, dayNightMutableObjects);
        
        var a = StartCoroutine(FadeToColor(color => sunSprite.color = color, sunSprite.color, (newTimeOfDay == DAY) ? DayColor : NightColor, TransitionDuration));
        var b = StartCoroutine(FadeToColor(color => mainCamera.backgroundColor = color, mainCamera.backgroundColor, (newTimeOfDay == DAY) ? DayBackgroundColor : NightBackgroundColor, TransitionDuration));

        yield return a;
        yield return b;
        
        DeactivateInvisibleMutables(newTimeOfDay, dayNightMutableObjects);
        LevelState.TimeOfDay = newTimeOfDay;
    }

    private static IEnumerator FadeToColor(Action<Color> applyColor, Color start, Color target, float duration) {
        var elapsed = 0f;
        while (elapsed < duration) {
            elapsed += Time.deltaTime;
            applyColor(Color.Lerp(start, target, elapsed / duration));
            yield return null;
        }
        applyColor(target);
    }

    private void ActivateVisibleMutables(TimeOfDay timeOfDay, List<DayNightMutableEntry> dayNightMutableEntries) {
        foreach (var dayNightMutableEntry in dayNightMutableEntries) {
            var go = dayNightMutableEntry.GameObject;
            var mutable = dayNightMutableEntry.Mutable;
            if (mutable.IsVisible(timeOfDay)) {
                go.SetActive(true);
            }
        }
    }

    private void DeactivateInvisibleMutables(TimeOfDay timeOfDay, List<DayNightMutableEntry> dayNightMutableEntries) { 
        foreach (var dayNightMutableEntry in dayNightMutableEntries) {
            var go = dayNightMutableEntry.GameObject;
            var mutable = dayNightMutableEntry.Mutable;
            if (!mutable.IsVisible(timeOfDay)) {
                go.SetActive(false);
            }
        }
    }
    
    /// initialization //
    private void InitLevelState() {
        LevelState = new LevelState();
        LevelState.Init();
    }
    
    private void InitPlayerState() {
        PlayerState = new PlayerState();
        PlayerState.Init();
    }
}