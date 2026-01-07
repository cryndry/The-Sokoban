using UnityEngine;

public interface ICanMove
{
    public Vector2Int GridPosition { get; set; }
    public bool IsMoving { get; set; }
    void MoveTo(Vector2Int targetPosition);
}