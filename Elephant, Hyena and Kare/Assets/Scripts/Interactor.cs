using UnityEngine;
 // https://theindieprofessor.wordpress.com/2025/01/07/crafting-2d-games-how-to-make-a-flexible-interaction-system-using-interfaces/
interface IInteractible
{
    public void Interact(GameObject CallSource);
}
 
public class Interactor : MonoBehaviour
{
    public Transform InteractorSource;
    public float InteractRange = 0.5f;
    public LayerMask InteractionMask;
 
 
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            CheckInteract();
        }
 
    }
 
    void CheckInteract()
    {
        var col = Physics2D.OverlapCircleAll(InteractorSource.transform.position, InteractRange, InteractionMask);
 
        if (col != null)
        {
            foreach (var c in col)
            {
                if (c.TryGetComponent(out IInteractible interactible))
                {
                    interactible.Interact(this.gameObject);
                }
            }
        }
    }
 
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(InteractorSource.transform.position, InteractRange);
    }
}