using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private PlayerVisuals visuals;
    [SerializeField] private Player player;
    [SerializeField] private SpriteRenderer sr;

    private const float frameRate = 0.1f;

    private float timer;
    private int currentFrameIndex;


    private void LateUpdate()
    {
        HandleSpriteUpdate();
    }

    private void HandleSpriteUpdate()
    {
        if (player.IsMoving)
        {
            PlayWalkAnimation(player.Direction);
        }
        else
        {
            SetIdleSprite(player.Direction);
            currentFrameIndex = 0;
            timer = 0;
        }
    }

    private void PlayWalkAnimation(Vector2Int dir)
    {
        Sprite[] currentAnimSet = null;

        if (dir == Vector2Int.left) currentAnimSet = visuals.leftWalkSprites;
        else if (dir == Vector2Int.right) currentAnimSet = visuals.rightWalkSprites;
        else if (dir == Vector2Int.up) currentAnimSet = visuals.upWalkSprites;
        else if (dir == Vector2Int.down) currentAnimSet = visuals.downWalkSprites;

        if (currentAnimSet == null || currentAnimSet.Length == 0) return;

        timer += Time.deltaTime;
        if (timer >= frameRate)
        {
            timer -= frameRate;
            currentFrameIndex = (currentFrameIndex + 1) % currentAnimSet.Length;
        }

        sr.sprite = currentAnimSet[currentFrameIndex];
    }

    private void SetIdleSprite(Vector2Int dir)
    {
        if (dir == Vector2Int.left) sr.sprite = visuals.leftIdleSprite;
        else if (dir == Vector2Int.right) sr.sprite = visuals.rightIdleSprite;
        else if (dir == Vector2Int.up) sr.sprite = visuals.upIdleSprite;
        else if (dir == Vector2Int.down) sr.sprite = visuals.downIdleSprite;
    }
}