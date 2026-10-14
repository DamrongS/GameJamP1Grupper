using UnityEngine;

public class disapeeringPlatform : MonoBehaviour
{
    public float timeToToggle = 2;
    public  float currentTime = 0;
    public bool enabeld = true;
    
    void Start()
    {
        enabeld = true;
    }
    
    void FixedUpdate() // a timer that goes to the timeToToggle amount and then resets
    {
        currentTime += Time.deltaTime;
        if (currentTime >= timeToToggle)
            {
            currentTime = 0;
            tooglePlatform();
            }
    }

    void tooglePlatform()
    {
        enabeld = !enabeld;
        foreach (Transform child in gameObject.transform)
        {
            if (child.tag != "Hyaena") // så that the Hyaena doesn't disappear when the platform is disabled 
            {
            child.gameObject.SetActive(enabeld);
            }

            if (child.tag != "Elephant") // -//-
            {
                child.gameObject.SetActive(enabeld);
            }
        }
    }
}
