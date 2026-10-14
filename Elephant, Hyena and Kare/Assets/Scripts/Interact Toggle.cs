using UnityEngine;
using UnityEngine.Events;
// https://theindieprofessor.wordpress.com/2025/01/07/crafting-2d-games-how-to-make-a-flexible-interaction-system-using-interfaces/2/
public class InteractToggle : MonoBehaviour, IInteractible
{
    public UnityEvent<bool> OnToggleBool;

    public bool isInteractable = true;
    
    [SerializeField]
    private bool isOn;

    public void Interact(GameObject Callsource)
    {
        if (isInteractable)
        {
            isOn = !isOn;
            OnToggleBool?.Invoke(isOn);
        }
    }

    public void ActivateTrigger()
    {
        isInteractable = true;
    }

    public void DeactivateTrigger()
    {
        isInteractable = false;
    }

    public void ToggleTrigger()
    {
        isInteractable = !isInteractable;
    }
}
