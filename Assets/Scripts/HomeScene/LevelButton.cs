using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelButton : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI levelButtonText;

    private int level;

    private void Awake()
    {
        level = PlayerPrefs.GetInt(PlayerPrefConstants.CURRENT_LEVEL_KEY, 1);
        levelButtonText.text = $"Level {level}";
    }

    public void OnButtonClick()
    {
        SceneManager.LoadScene(Scenes.LEVEL_SCENE);
    }
}