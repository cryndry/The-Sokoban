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
        EventManager.Instance.OnMove += OnMove;
        CameraController.Instance.SetTarget(transform);
    }

    private void OnDisable()
    {
        EventManager.Instance.OnMove -= OnMove;
        CameraController.Instance.SetTarget(null);
    }

    public void OnMove(Vector2Int direction)
    {
        if (IsMoving) return;

        this.direction = direction;
        GameAreaManager.Instance.HandlePlayerMove(this, GridPosition, direction);
    }

    public void MoveTo(Vector2Int targetPosition)
    {
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

        IsMoving = false;
    }
}