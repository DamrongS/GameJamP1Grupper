using UnityEngine;
using UnityEngine.Events;
 // https://theindieprofessor.wordpress.com/2025/01/07/crafting-2d-games-how-to-make-a-flexible-interaction-system-using-interfaces/2/
 // Here is an Toggleable script that allows any object or boolean value to be updated quickly.
public class InteractTrigger : MonoBehaviour, IInteractible
{
   public UnityEvent OnIrigger;
   public bool isInteractable, isRetriggerable;

   public void Interact(GameObject Callsource)
   {
      if (isInteractable)
      {
         if (!isRetriggerable)
         {
            isRetriggerable = false;
         }
         OnIrigger.Invoke();
      }
   }

   public void Activate()
   {
      isInteractable = true;
   }
   public void DeactivateTrigger()
   {
      isInteractable = !isInteractable;
   }
}
