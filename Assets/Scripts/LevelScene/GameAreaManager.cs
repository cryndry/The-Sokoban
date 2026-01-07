using System.Collections.Generic;
using UnityEngine;

public class GameAreaManager : LazySingleton<GameAreaManager>
{
    private readonly HashSet<Vector2Int> targetPositions = new();
    private Block[,] grid;
    private void Start()
    {
        LoadLevel();
        return;
    }

    private void LoadLevel()
    {
        LevelData levelData = LevelLoader.Instance.LoadLevelData();
        grid = new Block[levelData.grid_width, levelData.grid_height];
        for (int y = 0; y < levelData.grid_height; y++)
        {
            for (int x = 0; x < levelData.grid_width; x++)
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
                    new Vector3(x, -y, 0)
                );
            }
        }
    }
}