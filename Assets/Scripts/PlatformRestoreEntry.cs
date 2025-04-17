public class PlatformRestoreEntry {
    
    public Platform Platform { get; private set; }
    public VisibilityMode PreviousVisibilityMode { get; private set; }

    public PlatformRestoreEntry(Platform platform) {
        Platform = platform;
        PreviousVisibilityMode = platform.visibilityMode;
    }
}