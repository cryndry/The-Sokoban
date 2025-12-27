public class Wall : Block
{
    override public GameObjectTypes BlockType { get; } = GameObjectTypes.WALL;

    private void Awake()
    {
        sr.sprite = ThemeManager.Instance.ActiveLevelTheme.wallSprite;
    }
}