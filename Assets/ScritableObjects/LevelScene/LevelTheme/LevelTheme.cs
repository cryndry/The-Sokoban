using UnityEngine;

[CreateAssetMenu(
    fileName = "LevelTheme",
    menuName = "Scriptable Objects/LevelTheme"
)]
public class LevelTheme : ScriptableObject
{
    public Sprite wallSprite;
    public Sprite floorSprite;
    public Sprite targetSprite;
    public Sprite boxSprite;
    public Sprite boxOnTargetSprite;
}