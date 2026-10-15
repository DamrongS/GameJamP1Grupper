using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MapManager))]
public class MapManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (GUILayout.Button("Override PlayerPrefs"))
        {
            MapManager mapManager = (MapManager)target;
            mapManager.SavePlayerPrefs();
        }
    }
}