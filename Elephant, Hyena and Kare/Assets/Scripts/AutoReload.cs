using UnityEngine;
using UnityEngine.SceneManagement;

public class AutoReload : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Hyeana") || (other.gameObject.CompareTag("Elephant")))
        {
            
        }
    }
}
