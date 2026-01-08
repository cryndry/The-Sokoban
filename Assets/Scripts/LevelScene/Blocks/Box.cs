using UnityEngine;

public class Box : Block, ICanMove
{
    override public BlockType BlockType { get; } = BlockType.BOX;

    public Vector2Int GridPosition { get; set; }
    public bool IsMoving { get; set; } = false;

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

        GameAreaManager.Instance.HandleBoxMove(this, targetPosition);
        GridPosition = targetPosition;

        IsMoving = false;
    }
}