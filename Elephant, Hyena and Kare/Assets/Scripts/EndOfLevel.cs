using UnityEngine;
using UnityEngine.SceneManagement;

public class EndOfLevel : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Hyeana") || other.CompareTag("Elephant"))
        {
            CompleteLevel();
        }
    }

    public void CompleteLevel()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        int currentLevel = int.Parse(sceneName.Replace("Level_", ""));

        int mapLevel = currentLevel + 1;

        int levelsUnlocked = PlayerPrefs.GetInt("LevelsUnlocked", 1);

        int nextLevel = Mathf.Min(mapLevel + 1, 4);

        if (nextLevel > levelsUnlocked)
        {
            PlayerPrefs.SetInt("LevelsUnlocked", nextLevel);
        }

        PlayerPrefs.SetInt("LastLevelCompleted", mapLevel);
        PlayerPrefs.Save();

        SceneManager.LoadScene("LevelOverview");
    }
}