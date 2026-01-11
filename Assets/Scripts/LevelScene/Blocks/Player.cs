using UnityEngine;

public class Player : Block, ICanMove
{
    [SerializeField] private PlayerVisuals playerVisuals;

    override public BlockType BlockType { get; } = BlockType.PLAYER;
    public Vector2Int GridPosition { get; set; }
    public bool IsMoving { get; set; } = false;

    private Vector2Int direction = Vector2Int.down;
    private Vector3 startWorldPos;
    private Vector3 targetWorldPos;
    private float moveTimer = 0f;
    private const float moveDuration = 0.3f;


    private void OnEnable()
    {
        CameraController.Instance.SetTarget(transform);
    }

    private void OnDisable()
    {
        CameraController.Instance.SetTarget(null);
    }

    private void Update()
    {
        if (!IsMoving) return;
        
        moveTimer += Time.deltaTime;
        float t = moveTimer / moveDuration;
        transform.position = Vector3.Lerp(startWorldPos, targetWorldPos, t);

        if (moveTimer >= moveDuration)
        {
            transform.position = targetWorldPos;
            IsMoving = false;
        }
    }

    public void MoveTo(Vector2Int targetGridPos)
    {
        if (IsMoving) return;

        moveTimer = 0f;
        IsMoving = true;

        startWorldPos = transform.position;
        targetWorldPos = new Vector3(targetGridPos.x, targetGridPos.y, transform.position.z);

        direction = targetGridPos - GridPosition;
        GameAreaManager.Instance.HandlePlayerMove(this, targetGridPos);
        GridPosition = targetGridPos;
    }
}