using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Constants;
using static TimeOfDay;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }

    public LevelState LevelState { get; private set; }
    public PlayerState PlayerState { get; private set; }

    public Camera mainCamera;
    public SpriteRenderer sunSprite;

    private void Awake()
    {
        Instance = this;
        InitializeLevelState();
        InitializePlayerState();
    }

    private void OnEnable()
    {
        InputHandler.ToggleDayNightPressed += HandleToggleDayNight;
    }

    private void OnDisable()
    {
        InputHandler.ToggleDayNightPressed -= HandleToggleDayNight;
    }

    private void HandleToggleDayNight()
    {
        if (LevelState.TimeOfDay == ECLIPSE) return;
        StartCoroutine(ToggleDayNightRoutine());
    }

    private IEnumerator ToggleDayNightRoutine()
    {
        var current = LevelState.TimeOfDay;
        var next = (current == DAY) ? NIGHT : DAY;

        LevelState.TimeOfDay = ECLIPSE;

        var mutables = LevelState.GetDayNightMutableObjects();
        SetMutablesVisibility(mutables, next, true);

        var sunTargetColor = GetTargetColor(next, true);
        var bgTargetColor = GetTargetColor(next, false);

        var sunFade = StartCoroutine(ApplyColorTransition(sunSprite.color, sunTargetColor, TransitionDuration, c => sunSprite.color = c));
        var bgFade = StartCoroutine(ApplyColorTransition(mainCamera.backgroundColor, bgTargetColor, TransitionDuration, c => mainCamera.backgroundColor = c));

        yield return sunFade;
        yield return bgFade;

        SetMutablesVisibility(mutables, next, false);
        LevelState.TimeOfDay = next;
    }

    private IEnumerator ApplyColorTransition(Color start, Color end, float duration, Action<Color> apply)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            apply(Color.Lerp(start, end, elapsed / duration));
            yield return null;
        }
        apply(end);
    }

    private Color GetTargetColor(TimeOfDay timeOfDay, bool isSun)
    {
        return timeOfDay == DAY
            ? (isSun ? DayColor : DayBackgroundColor)
            : (isSun ? NightColor : NightBackgroundColor);
    }

    private void SetMutablesVisibility(List<DayNightMutableEntry> entries, TimeOfDay timeOfDay, bool activating)
    {
        foreach (var entry in entries)
        {
            bool shouldBeActive = entry.Mutable.IsVisible(timeOfDay);
            if (shouldBeActive == activating)
            {
                entry.GameObject.SetActive(activating);
            }
        }
    }

    /// <summary>
    /// Initialization
    /// </summary>
    private void InitializeLevelState()
    {
        LevelState = new LevelState();
        LevelState.Init();
    }

    private void InitializePlayerState()
    {
        PlayerState = new PlayerState();
        PlayerState.Init();
    }
}