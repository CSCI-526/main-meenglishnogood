using Unity.VisualScripting;

public class PlayerState {

    public int NumStars { get; set; } = 0;
    public int NumAbilities { get; set; } = 0;
    public Size Size { get; set; } = Size.BIG;
    public Portal CurrentPortal { get; set; } = null;
    public GravityMode GravityMode { get; set; } = GravityMode.NORMAL;
    
    public bool IsGrounded { get; set; } = false;
    public bool IsFalling { get; set; } = false;
    public bool InPortal { get; set; } = false;

    public bool IsBig() {
        return Size == Size.BIG;
    }

    public bool IsSmall() {
        return Size == Size.SMALL;
    }

    public void Rollback(Checkpoint checkpoint) {
        NumStars = checkpoint.NumStars;
        NumAbilities = checkpoint.NumAbilities;
        Size = checkpoint.PlayerSize;
        GravityMode = checkpoint.PlayerGravityMode;
    }
}