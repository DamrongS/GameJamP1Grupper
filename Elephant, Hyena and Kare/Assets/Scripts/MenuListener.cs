using Unity.VectorGraphics;
using UnityEngine;

public class MenuListener : MonoBehaviour
{
    public void ResumeGame()
    {
        Debug.Log("Resume");
    }

    public void RestartGame(string SceneName)
    {
        Debug.Log($"Restart {SceneName}");
    }

    public void ExitGame(string SceneName)
    {
        Debug.Log($"Exit {SceneName}");
    }
}
