using Unity.VisualScripting;

public class PlayerState {

    public int NumStars { get; set; }
    public int NumAbilities { get; set; }
    public Size Size { get; set; }
    public Portal CurrentPortal { get; set; }
    public GravityMode GravityMode { get; set; }
    
    public bool IsGrounded { get; set; } = false;
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

    public void Init() {
        NumStars = 0;
        NumAbilities = 0;
        Size = Size.BIG;
        CurrentPortal = null;
        GravityMode = GravityMode.NORMAL;
    }
}