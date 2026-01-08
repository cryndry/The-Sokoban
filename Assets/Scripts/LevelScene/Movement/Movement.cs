using UnityEngine;

public class Movement
{
    public ICanMove Mover { get; private set; }
    public Vector2Int From { get; private set; }
    public Vector2Int To { get; private set; }

    public Movement(ICanMove mover, Vector2Int from, Vector2Int to)
    {
        Mover = mover;
        From = from;
        To = to;
    }
}