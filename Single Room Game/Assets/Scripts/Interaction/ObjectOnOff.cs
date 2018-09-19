using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectOnOff :MonoBehaviour {

    public void Toggle(GameObject obj)
    {
        obj.SetActive(!obj.activeSelf);
    }

    public void TurnOn(GameObject obj)
    {
        obj.SetActive(true);
    }

    public void TurnOff(GameObject obj)
    {
        obj.SetActive(false);
    }

}
