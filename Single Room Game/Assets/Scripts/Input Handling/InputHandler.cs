using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InputHandler : MonoBehaviour {

    [System.Serializable]
    public class AxisUpdate : UnityEvent<float> { };

    [System.Serializable]
    public class ButtonUpdate : UnityEvent<bool> { };

    [SerializeField]
    private AxisUpdate Walk_Vertical, Walk_Horizontal, Look_X, Look_Y;

    [SerializeField]
    private ButtonUpdate Interact;

    private Dictionary<string, float> lastAxisValue;

    void Start()
    {
        lastAxisValue = new Dictionary<string, float>();

        lastAxisValue["Vertical"] = 0;
        lastAxisValue["Horizontal"] = 0;
        lastAxisValue["Look X"] = 0;
        lastAxisValue["Look Y"] = 0;
        lastAxisValue["Interact"] = 0;
    }

    void Update()
    {
        HandleInput();
        UpdateKeyValues();
    }

    private void HandleInput()
    {
        Walk_Vertical.Invoke(Input.GetAxisRaw("Vertical"));
        Walk_Horizontal.Invoke(Input.GetAxisRaw("Horizontal"));
        Look_X.Invoke(Input.GetAxisRaw("Look X"));
        Look_Y.Invoke(Input.GetAxisRaw("Look Y"));

        Interact.Invoke(GetKeyDown("Interact"));
    }

    private void UpdateKeyValues()
    {
        List<string> keyList = new List<string>(lastAxisValue.Keys);
        foreach(string Key in keyList)
        {
            lastAxisValue[Key] = Input.GetAxis(Key);
        }
    }

    private bool GetKeyDown(string key)
    {
        if(lastAxisValue.ContainsKey(key))
        {
            return lastAxisValue[key] == 0 && Input.GetAxis(key) == 1;
        }

        return false;
    }

    public void SetListenerInteract(UnityAction<bool> call)
    {
        Interact.AddListener(call);
    }

    public void RemoveAllListenersInteract()
    {
        Interact.RemoveAllListeners();
    }

}
