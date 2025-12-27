using UnityEngine;

public class ThemeManager : LazySingleton<ThemeManager>
{
    [SerializeField] private LevelTheme[] levelThemes;

    public LevelTheme ActiveLevelTheme { get; private set; }

    private void Awake()
    {
        ActiveLevelTheme = levelThemes[Random.Range(0, levelThemes.Length)];
    }
}