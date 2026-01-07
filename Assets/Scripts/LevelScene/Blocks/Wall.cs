public class Wall : Block
{
    override public BlockType BlockType { get; } = BlockType.WALL;

    private void Awake()
    {
        sr.sprite = ThemeManager.Instance.ActiveLevelTheme.wallSprite;
    }
}