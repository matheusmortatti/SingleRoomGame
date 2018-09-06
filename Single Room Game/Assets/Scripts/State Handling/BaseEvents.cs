using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BaseEvents {

    UnityEvent CallbackOnTrigger;

    public void Invoke()
    {
        CallbackOnTrigger.Invoke();
    }
	
}
