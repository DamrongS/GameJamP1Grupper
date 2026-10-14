using UnityEngine;
// https://theindieprofessor.wordpress.com/2025/01/07/crafting-2d-games-how-to-make-a-flexible-interaction-system-using-interfaces/2/
public class InteractTest : MonoBehaviour, IInteractible
{
    //Requires a 2D Collider like Box2D and set their layermask to interact
 
    public void Interact(GameObject CallSource)
    {
        Debug.Log(Random.Range(0.0f, 100.0f));
    }
 
}