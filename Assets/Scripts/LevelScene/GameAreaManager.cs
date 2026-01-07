using System.Collections.Generic;
using UnityEngine;

public class GameAreaManager : LazySingleton<GameAreaManager>
{
    private readonly HashSet<Vector2Int> targetPositions = new();
    private Block[,] grid;
    private int gridWidth;
    private int gridHeight;


    private void Start()
    {
        LoadLevel();
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

                grid[x, y] = BlockGenerator.Instance.GenerateBlock(
                    blockType,
                    new Vector3(x, y, 0)
                );
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

    public void HandlePlayerMove(Player player, Vector2Int gridPosition, Vector2Int direction)
    {
        Vector2Int targetPosition = gridPosition + direction;

        if (!IsValidPosition(targetPosition)) return;

        Block targetBlock = GetBlockAtPosition(targetPosition);

        if (targetBlock == null)
        {
            ClearBlockAtPosition(gridPosition);
            SetBlockAtPosition(targetPosition, player);
            player.GridPosition = targetPosition;
            player.MoveTo(targetPosition);

            CheckLevelCompletion();
        }
        else if (targetBlock is Box box)
        {
            Vector2Int nextPosition = targetPosition + direction;

            if (!IsValidPosition(nextPosition)) return;

            Block nextBlock = GetBlockAtPosition(nextPosition);
            if (nextBlock == null)
            {
                ClearBlockAtPosition(gridPosition);
                ClearBlockAtPosition(targetPosition);

                SetBlockAtPosition(nextPosition, box);
                box.MoveTo(nextPosition);
                box.IsOnTarget = targetPositions.Contains(nextPosition);

                SetBlockAtPosition(targetPosition, player);
                player.GridPosition = targetPosition;
                player.MoveTo(targetPosition);

                CheckLevelCompletion();
            }
        }
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