using UnityEngine;

public class ThemeManager : LazySingleton<ThemeManager>
{
    [SerializeField] private SpriteRenderer backgroundRenderer;
    [SerializeField] private LevelTheme[] levelThemes;

    public LevelTheme ActiveLevelTheme { get; private set; }

    private void Awake()
    {
        ActiveLevelTheme = levelThemes[Random.Range(0, levelThemes.Length)];
        backgroundRenderer.sprite = ActiveLevelTheme.floorSprite;
    }
}