using UnityEngine;
using UnityEngine.SceneManagement;

public class EndOfGame : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Elephant") || col.CompareTag("Hyeana"))
        SceneManager.LoadScene("EndingCutscene");
    }
}
