using UnityEngine;

[CreateAssetMenu(fileName = "PlayerVisuals", menuName = "Scriptable Objects/PlayerVisuals")]
public class PlayerVisuals : ScriptableObject
{
    public Sprite leftIdleSprite;
    public Sprite[] leftWalkSprites;

    public Sprite rightIdleSprite;
    public Sprite[] rightWalkSprites;

    public Sprite upIdleSprite;
    public Sprite[] upWalkSprites;

    public Sprite downIdleSprite;
    public Sprite[] downWalkSprites;
}
