public class ChangedPlatformEntry {
    
    public Platform Platform { get; private set; }
    public VisibilityMode PreviousVisibilityMode { get; private set; }

    public ChangedPlatformEntry(Platform platform, VisibilityMode visibilityMode) {
        Platform = platform;
        PreviousVisibilityMode = visibilityMode;
    }
}