using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class RaycastObjectEvent : RaycastEvent {

    public GameObject InteractableObject;
    public ObjectEvent OnRaycastEnter, OnRaycastStays, OnRaycastExit;

    public override void InvokeEnter()
    {
        OnRaycastEnter.Invoke(InteractableObject);
    }

    public override void InvokeStay()
    {
        OnRaycastStays.Invoke(InteractableObject);
    }

    public override void InvokeExit()
    {
        OnRaycastExit.Invoke(InteractableObject);
    }

}
