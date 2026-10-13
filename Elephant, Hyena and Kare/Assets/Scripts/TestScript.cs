using UnityEngine;
using UnityEngine.SceneManagement;

public class TestScript : MonoBehaviour
{
    public void Playgame()
    {
        SceneManager.LoadSceneAsync("level 1");
    }

     
}
