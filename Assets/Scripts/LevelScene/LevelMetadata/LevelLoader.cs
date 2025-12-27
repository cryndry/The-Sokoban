using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LevelLoader : LazySingleton<LevelLoader>
{
    private readonly Dictionary<char, GameObjectTypes> tileMappings = new();


    private void Awake()
    {
        System.Array enumValues = System.Enum.GetValues(typeof(GameObjectTypes));
        foreach (GameObjectTypes type in enumValues)
        {
            char key = (char)type;
            tileMappings.Add(key, type);
        }
    }

    public LevelData LoadLevelData()
    {
        int level = PlayerPrefs.GetInt(LevelSceneConstants.CURRENT_LEVEL_KEY, 1);
        string levelFileName = $"{LevelSceneConstants.LEVEL_FILE_PREFIX}{level}.{LevelSceneConstants.LEVEL_FILE_EXTENSION}";
        string levelFilePath = Path.Combine(
            Application.dataPath,
            LevelSceneConstants.LEVELS_FOLDER,
            levelFileName
        );

        if (File.Exists(levelFilePath))
        {
            string jsonContent = File.ReadAllText(levelFilePath);
            LevelJson levelJson = JsonUtility.FromJson<LevelJson>(jsonContent);
            LevelData levelData = ParseLevelJson(levelJson);
            return levelData;
        }
        else
        {
            Debug.LogError("Level JSON not found: " + levelFilePath);
            return null;
        }
    }

    private LevelData ParseLevelJson(LevelJson levelJson)
    {
        LevelData levelData = new LevelData
        {
            grid_width = levelJson.width,
            grid_height = levelJson.height,
            grid = new GameObjectTypes[levelJson.height, levelJson.width],
        };

        for (int i = 0; i < levelJson.height; i++)
        {
            string row = levelJson.grid[i];
            for (int j = 0; j < levelJson.width; j++)
            {
                levelData.grid[i, j] = j < row.Length ? tileMappings[row[j]] : GameObjectTypes.FLOOR;
            }
        }

        return levelData;
    }
}