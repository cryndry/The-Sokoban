using UnityEngine;

public class Box : Block, ICanMove
{
    override public GameObjectTypes BlockType { get; } = GameObjectTypes.BOX;

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

    public void Move(Vector2Int direction)
    {
        transform.position += new Vector3(direction.x, direction.y, 0);
    }
}