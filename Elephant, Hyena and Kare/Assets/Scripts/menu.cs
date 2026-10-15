using UnityEngine;
using UnityEngine.SceneManagement;

public class menu : MonoBehaviour
{
    public void Playgame()
    {
        if (!PlayerPrefs.HasKey("LevelsUnlocked"))
        {
            PlayerPrefs.SetInt("LevelsUnlocked", 1);
            PlayerPrefs.SetInt("LastLevelCompleted", 0);
            PlayerPrefs.Save();
        }

        SceneManager.LoadScene("LevelOverview");
    }
}