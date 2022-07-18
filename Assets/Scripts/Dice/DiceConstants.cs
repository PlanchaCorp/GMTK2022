public enum DiceDirections {
    NONE, TOP, RIGHT, DOWN, LEFT
}

public static class DicesStates {
    public static readonly string Idle = "Idle";
    public static readonly string Moving = "Moving";
    public static readonly string Locked = "Locked";
}

public static class DicesTransitions {
    public static readonly string BeginMovement = "BeginMovement";
    public static readonly string EndMovement = "EndMovement";
    public static readonly string LockDices = "LockDices";
}
