using UnityEngine;

public class Box : Block, ICanMove
{
    override public BlockType BlockType { get; } = BlockType.BOX;

    public Vector2Int GridPosition { get; set; }
    public bool IsMoving { get; set; } = false;

    private Vector3 startWorldPos;
    private Vector3 targetWorldPos;
    private float moveTimer = 0f;
    private const float moveDuration = 0.3f;

    private bool isOnTarget = false;
    public bool IsOnTarget
    {
        get => isOnTarget;
        set
        {
            isOnTarget = value;
            if (value)
            {
                sr.sprite = ThemeManager.Instance.ActiveLevelTheme.boxOnTargetSprite;
            }
            else
            {
                sr.sprite = ThemeManager.Instance.ActiveLevelTheme.boxSprite;
            }
        }
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

        GameAreaManager.Instance.HandleBoxMove(this, targetGridPos);
        GridPosition = targetGridPos;
    }
}