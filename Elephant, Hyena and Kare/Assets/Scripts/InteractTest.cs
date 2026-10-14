using UnityEngine;
 
public class InteractTest : MonoBehaviour, IInteractible
{
    //Requires a 2D Collider like Box2D and set their layermask to interact
 
    public void Interact(GameObject CallSource)
    {
        Debug.Log(Random.Range(0.0f, 100.0f));
    }
 
}