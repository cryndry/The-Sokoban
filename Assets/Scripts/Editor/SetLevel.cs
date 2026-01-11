using UnityEditor;
using UnityEngine;

public class SetLevelInputWindow : EditorWindow
{
    private int level;

    [MenuItem("Set Level/Set Level")]
    public static void ShowWindow()
    {
        SetLevelInputWindow window = GetWindow<SetLevelInputWindow>("Set Level");
        window.level = PlayerPrefs.GetInt(PlayerPrefConstants.CURRENT_LEVEL_KEY, 1);
    }

    private void OnGUI()
    {

        GUILayout.Label("Set Level", EditorStyles.boldLabel);
        level = EditorGUILayout.IntField("Level:", level);

        if (GUILayout.Button("Save"))
        {
            PlayerPrefs.SetInt(PlayerPrefConstants.CURRENT_LEVEL_KEY, level);
            PlayerPrefs.Save();
            Close();
        }
    }
}
