using System.Collections;

using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroChange : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(IntroTimer());
    }

    IEnumerator IntroTimer()
    {
        Debug.Log("Starting timer");
        yield return new WaitForSeconds(13.5f);


        SceneManager.LoadScene("LevelOverview");
    }

}
