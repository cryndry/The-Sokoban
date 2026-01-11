using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : LazySingleton<LevelManager>
{
    private void OnEnable()
    {
        EventManager.Instance.OnLevelComplete += NextLevel;
    }

    private void OnDisable()
    {
        EventManager.Instance.OnLevelComplete -= NextLevel;
    }

    private void NextLevel()
    {
        int level = PlayerPrefs.GetInt(LevelSceneConstants.CURRENT_LEVEL_KEY, 1);
        PlayerPrefs.SetInt(LevelSceneConstants.CURRENT_LEVEL_KEY, level + 1);
        
        SceneManager.LoadScene(LevelSceneConstants.LEVEL_SCENE_NAME);
    }
}