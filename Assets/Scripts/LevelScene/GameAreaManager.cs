using System.Collections.Generic;
using UnityEngine;

public class GameAreaManager : LazySingleton<GameAreaManager>
{
    private readonly HashSet<Vector2Int> targetPositions = new();
    private Block[,] grid;
    private int gridWidth;
    private int gridHeight;

    private Player player;


    private void Start()
    {
        LoadLevel();
    }

    private void OnEnable()
    {
        EventManager.Instance.OnMove += HandleMoveInput;
    }

    private void OnDisable()
    {
        EventManager.Instance.OnMove -= HandleMoveInput;
    }

    private void LoadLevel()
    {
        LevelData levelData = LevelLoader.Instance.LoadLevelData();
        gridHeight = levelData.grid_height;
        gridWidth = levelData.grid_width;

        grid = new Block[gridWidth, gridHeight];
        for (int y = 0; y < gridHeight; y++)
        {
            for (int x = 0; x < gridWidth; x++)
            {
                BlockType blockType = levelData.grid[x, y];

                if (blockType == BlockType.TARGET ||
                    blockType == BlockType.BOX_ON_TARGET ||
                    blockType == BlockType.PLAYER_ON_TARGET)
                {
                    targetPositions.Add(new Vector2Int(x, y));
                }

                Block block = BlockGenerator.Instance.GenerateBlock(
                    blockType,
                    new Vector3(x, y, 0)
                );

                grid[x, y] = block;

                if (block is ICanMove movable)
                {
                    movable.GridPosition = new Vector2Int(x, y);

                    if (block is Player p)
                    {
                        player = p;
                    }
                }
            }
        }
    }

    private bool IsValidPosition(Vector2Int position)
    {
        return position.x >= 0 && position.x < gridWidth &&
               position.y >= 0 && position.y < gridHeight;
    }

    private Block GetBlockAtPosition(Vector2Int position)
    {
        if (!IsValidPosition(position)) return null;

        return grid[position.x, position.y];
    }

    private void SetBlockAtPosition(Vector2Int position, Block block)
    {
        if (!IsValidPosition(position)) return;

        grid[position.x, position.y] = block;
    }

    private void ClearBlockAtPosition(Vector2Int position)
    {
        if (!IsValidPosition(position)) return;

        grid[position.x, position.y] = null;
    }

    public void HandleMoveInput(Vector2Int direction)
    {
        Vector2Int targetPosition = player.GridPosition + direction;

        if (!IsValidPosition(targetPosition)) return;

        List<Movement> turnMovements = new List<Movement>();

        Block targetBlock = GetBlockAtPosition(targetPosition);

        if (targetBlock == null)
        {
            turnMovements.Add(new Movement(player, player.GridPosition, targetPosition));
            player.MoveTo(targetPosition);
        }
        else if (targetBlock is Box box)
        {
            Vector2Int nextPosition = targetPosition + direction;

            if (!IsValidPosition(nextPosition)) return;

            Block nextBlock = GetBlockAtPosition(nextPosition);

            if (nextBlock == null)
            {
                turnMovements.Add(new Movement(box, box.GridPosition, nextPosition));
                box.MoveTo(nextPosition);

                turnMovements.Add(new Movement(player, player.GridPosition, targetPosition));
                player.MoveTo(targetPosition);

                CheckLevelCompletion();
            }
        }

        if (turnMovements.Count > 0)
        {
            MovementManager.Instance.RegisterMovement(turnMovements);
        }
    }

    public void HandlePlayerMove(Player player, Vector2Int targetPosition)
    {
        if (GetBlockAtPosition(player.GridPosition) == player)
        {
            ClearBlockAtPosition(player.GridPosition);
        }

        SetBlockAtPosition(targetPosition, player);
    }

    public void HandleBoxMove(Box box, Vector2Int targetPosition)
    {
        if (GetBlockAtPosition(box.GridPosition) == box)
        {
            ClearBlockAtPosition(box.GridPosition);
        }

        SetBlockAtPosition(targetPosition, box);
        box.IsOnTarget = targetPositions.Contains(targetPosition);
    }

    private void CheckLevelCompletion()
    {
        foreach (Vector2Int targetPos in targetPositions)
        {
            Block block = GetBlockAtPosition(targetPos);
            if (block is not Box box || !box.IsOnTarget)
            {
                return;
            }
        }

        EventManager.Instance.InvokeOnLevelComplete();
    }
}