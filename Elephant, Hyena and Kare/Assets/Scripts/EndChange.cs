using System.Collections;

using UnityEngine;
using UnityEngine.SceneManagement;

public class EndChange : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(IntroTimer());
    }

    IEnumerator IntroTimer()
    {
        Debug.Log("Starting timer");
        yield return new WaitForSeconds(18f);


        SceneManager.LoadScene("Menu");
    }

}