using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public abstract class RaycastEvent : MonoBehaviour {

    public abstract void InvokeEnter();
    public abstract void InvokeStay();
    public abstract void InvokeExit();

}
