using System;
using UnityEngine;

public class Teleporter : MonoBehaviour, IInteractible
{
    public GameObject Teleport;

    public void TeleportTo(GameObject obj)
    {
        obj.transform.position = Teleport.transform.position;
    }

    public void Interact(GameObject CallSource)
    {
        TeleportTo(CallSource);
    }
}