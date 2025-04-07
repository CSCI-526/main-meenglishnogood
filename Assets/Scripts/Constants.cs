using UnityEngine;

public static class Constants {
    
    public const float GrowFactor = 2.0f;
    public const float ShrinkFactor = 0.5f;
    public const float RespawnDelay = 2.0f;
    public const float AntiGravityScale = -1.0f;
    public const float NormalGravityScale = 1.0f;
    public const float TransitionDuration = 2.0f;
    public const float AnalyticsLogCoolDownTime = 1.0f;
    public const float PlayerHorizontalVelocity = 5.0f;
    public const float PlayerJumpVerticalVelocity = 6.5f;
    public const float AbilityInstantiateDistance = 1.2f;
    public const float CollectibleRotationSpeed = 100.0f;
    public static readonly Color DayColor = Color.yellow;
    public static readonly Color NightColor = Color.black;
    public static readonly Color DayBackgroundColor = new(0.5f, 0.8f, 1.0f);
    public static readonly Color NightBackgroundColor = new(0.05f, 0.05f, 0.2f);
    public static readonly Color PersistentColor = new(0.3686275f, 0.3686275f, 0.3686275f);
}