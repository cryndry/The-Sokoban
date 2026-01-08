using UnityEngine;

public class Player : Block, ICanMove
{
    [SerializeField] private PlayerVisuals playerVisuals;

    override public BlockType BlockType { get; } = BlockType.PLAYER;
    public Vector2Int GridPosition { get; set; }
    public bool IsMoving { get; set; } = false;

    private Vector2Int direction = Vector2Int.down;

    private void OnEnable()
    {
        CameraController.Instance.SetTarget(transform);
    }

    private void OnDisable()
    {
        CameraController.Instance.SetTarget(null);
    }

    public void MoveTo(Vector2Int targetPosition)
    {
        if (IsMoving) return;

        this.direction = targetPosition - GridPosition;

        IsMoving = true;

        // Simple instant move for now; can be replaced with smooth movement later
        transform.position = Vector3.MoveTowards(
            transform.position,
            new Vector3(
                targetPosition.x,
                targetPosition.y,
                transform.position.z
            ),
            1f
        );

        GameAreaManager.Instance.HandlePlayerMove(this, targetPosition);
        GridPosition = targetPosition;

        IsMoving = false;
    }
}