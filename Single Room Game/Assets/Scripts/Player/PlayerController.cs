using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour {

    [SerializeField]
    private float WalkSpeed = 5.0f;
    [SerializeField]
    private float LookSensitivity = 5.0f;
    [SerializeField]
    private Text interactionText;
    [SerializeField]
    private InputStateEvents[] baseInputStateEvents;

    private string interactionMessage = "";

    private InputStateEvents currentInputEvent;

    private UnityEvent InteractionEvents;

    private PlayerMotor motor;

    private float _xMov, _zMov, _xRot, _yRot;
    private InteractionStateHandler interactionStateHandler;

    private AttachToItself playerHand;
    private InputHandler inputHandler;

    void Start()
    {
        motor = GetComponent<PlayerMotor>();

        interactionStateHandler = FindObjectOfType<InteractionStateHandler>();

        playerHand = GetComponentInChildren<AttachToItself>();

        inputHandler = GetComponentInChildren<InputHandler>();

        currentInputEvent = new InputStateEvents();
        UpdateEvents();
    }

    void Update()
    {
        UpdateMovement();
        UpdateEvents();

        interactionText.text = interactionMessage;
        interactionMessage = "";
    }

    private void UpdateMovement()
    {
        Vector3 _movHorizontal = transform.right * _xMov;
        Vector3 _movVertical = transform.forward * _zMov;

        Vector3 _velocity = (_movHorizontal + _movVertical).normalized * WalkSpeed;

        Vector3 _rotation = new Vector3(-_xRot, _yRot, 0) * LookSensitivity;

        motor.ApplyVelocity(_velocity);
        motor.ApplyRotation(_rotation);
    }

    public void InteractionMessage(string message)
    {
        interactionMessage = message;
    }

    private void UpdateEvents()
    {
        currentInputEvent = FindCurrentEvent();
    }

    public void ProcessZoomInAxis(float val)
    {
        if (currentInputEvent.AltZoomInEvent.eventCounter == 0)
        {
            currentInputEvent.ZoomInEvent.Invoke(val);
        }
        else
        {
            currentInputEvent.AltZoomInEvent.axisEvent.Invoke(val);
            RemoveAllAltListenersZoomIn();
        }
    }

    public void ProcessVerticalAxis(float val)
    {
        if (currentInputEvent.AltVerticalEvent.eventCounter == 0)
        {
            currentInputEvent.VerticalEvent.Invoke(val);
        }
        else
        {
            currentInputEvent.AltVerticalEvent.axisEvent.Invoke(val);
            RemoveAllAltListenersVertical();
        }
    }

    public void ProcessHorizontalAxis(float val)
    {
        if (currentInputEvent.AltHorizontalEvent.eventCounter == 0)
        {
            currentInputEvent.HorizontalEvent.Invoke(val);
        }
        else
        {
            currentInputEvent.AltHorizontalEvent.axisEvent.Invoke(val);
            RemoveAllAltListenersHorizontal();
        }
    }

    public void ProcessLookX(float val)
    {
        if (currentInputEvent.AltLookXEvent.eventCounter == 0)
        {
            currentInputEvent.LookXEvent.Invoke(val);
        }
        else
        {
            currentInputEvent.AltLookXEvent.axisEvent.Invoke(val);
            RemoveAllAltListenersLookX();
        }
    }

    public void ProcessLookY(float val)
    {
        if (currentInputEvent.AltLookYEvent.eventCounter == 0)
        {
            currentInputEvent.LookYEvent.Invoke(val);
        }
        else
        {
            currentInputEvent.AltLookYEvent.axisEvent.Invoke(val);
            RemoveAllAltListenersLookY();
        }
    }

    public void ProcessInteract(bool buttonValue)
    {
        // Interact with objects
        if (buttonValue)
        {
            if (currentInputEvent.AltInteractionEvent.eventCounter == 0)
            {
                currentInputEvent.InteractionEvent.Invoke();
            }
            else
            {
                currentInputEvent.AltInteractionEvent.unityEvent.Invoke();
            }
        }

        RemoveAllAltListenersInteract();
    }


    public void ApplyVerticalMovement(float val)
    {
        _zMov = val;
    }

    public void ApplyHorizontalMovement(float val)
    {
        _xMov = val;
    }

    public void ApplyLookX(float val)
    {
        _yRot = val;
    }

    public void ApplyLookY(float val)
    {
        _xRot = val;
    }

    public void SetAltListenerInteract(UnityAction call)
    {
        currentInputEvent.AltInteractionEvent.unityEvent.AddListener(call);
        currentInputEvent.AltInteractionEvent.eventCounter++;
    }

    public void RemoveAllAltListenersInteract()
    {
        currentInputEvent.AltInteractionEvent.unityEvent.RemoveAllListeners();
        currentInputEvent.AltInteractionEvent.eventCounter = 0;
    }

    public void SetAltListenerHorizontal(UnityAction<float> call)
    {
        currentInputEvent.AltHorizontalEvent.axisEvent.AddListener(call);
        currentInputEvent.AltHorizontalEvent.eventCounter++;
    }

    public void RemoveAllAltListenersHorizontal()
    {
        currentInputEvent.AltHorizontalEvent.axisEvent.RemoveAllListeners();
        currentInputEvent.AltHorizontalEvent.eventCounter = 0;
    }

    public void SetAltListenerVertical(UnityAction<float> call)
    {
        currentInputEvent.AltVerticalEvent.axisEvent.AddListener(call);
        currentInputEvent.AltVerticalEvent.eventCounter++;
    }

    public void RemoveAllAltListenersVertical()
    {
        currentInputEvent.AltVerticalEvent.axisEvent.RemoveAllListeners();
        currentInputEvent.AltVerticalEvent.eventCounter = 0;
    }

    public void SetAltListenerLookX(UnityAction<float> call)
    {
        currentInputEvent.AltLookXEvent.axisEvent.AddListener(call);
        currentInputEvent.AltLookXEvent.eventCounter++;
    }

    public void RemoveAllAltListenersLookX()
    {
        currentInputEvent.AltLookXEvent.axisEvent.RemoveAllListeners();
        currentInputEvent.AltLookXEvent.eventCounter = 0;
    }

    public void SetAltListenerLookY(UnityAction<float> call)
    {
        currentInputEvent.AltLookYEvent.axisEvent.AddListener(call);
    }

    public void RemoveAllAltListenersLookY()
    {
        currentInputEvent.AltLookYEvent.axisEvent.RemoveAllListeners();
    }

    public void SetAltListenerZoomIn(UnityAction<float> call)
    {
        currentInputEvent.AltZoomInEvent.axisEvent.AddListener(call);
    }

    public void RemoveAllAltListenersZoomIn()
    {
        currentInputEvent.AltZoomInEvent.axisEvent.RemoveAllListeners();
    }

    private InputStateEvents FindCurrentEvent()
    {
        foreach(InputStateEvents e in baseInputStateEvents)
        {
            if(e.state == interactionStateHandler.GetCurrentState())
            {
                return e;
            }
        }

        return new InputStateEvents();
    }
}

[System.Serializable]
public class InputStateEvents
{
    public InputStateEvents()
    {
        VerticalEvent = new AxisUpdate();
        HorizontalEvent = new AxisUpdate();
        LookXEvent = new AxisUpdate();
        LookYEvent = new AxisUpdate();
        ZoomInEvent = new AxisUpdate();
        InteractionEvent = new UnityEvent();

        AltHorizontalEvent = new AxisEventParams();
        AltVerticalEvent = new AxisEventParams();
        AltLookXEvent = new AxisEventParams();
        AltLookYEvent = new AxisEventParams();
        AltZoomInEvent = new AxisEventParams();
        AltInteractionEvent = new UnityEventParams();
    }

    public InteractionStateNames state;

    public AxisUpdate VerticalEvent, HorizontalEvent, LookXEvent, LookYEvent, ZoomInEvent;
    public UnityEvent InteractionEvent;

    [HideInInspector]
    public AxisEventParams AltVerticalEvent, AltHorizontalEvent, AltLookXEvent, AltLookYEvent, AltZoomInEvent;
    [HideInInspector]
    public UnityEventParams AltInteractionEvent;

    public class UnityEventParams
    {
        public UnityEvent unityEvent;
        public int eventCounter;

        public UnityEventParams()
        {
            unityEvent = new UnityEvent();
            eventCounter = 0;
        }
    }

    public class AxisEventParams
    {
        public AxisUpdate axisEvent;
        public int eventCounter;

        public AxisEventParams()
        {
            axisEvent = new AxisUpdate();
            eventCounter = 0;
        }
    }

    //public object Clone()
    //{
    //    InputStateEvents newState = new InputStateEvents();

    //    newState.state = this.state;

    //    for(int i = 0; i < VerticalEvent.GetPersistentEventCount(); i++)
    //    {
    //        MethodInfo info = UnityEventBase.GetValidMethodInfo(this.VerticalEvent.GetPersistentTarget(i), this.VerticalEvent.GetPersistentMethodName(i), new Type[] { typeof(float) });
    //        UnityAction<float> execute = (float f) => info.Invoke(this.VerticalEvent.GetPersistentTarget(i), new object[] { f });
    //        newState.VerticalEvent.AddListener(execute);
    //    }

    //    for (int i = 0; i < HorizontalEvent.GetPersistentEventCount(); i++)
    //    {
    //        MethodInfo info = UnityEventBase.GetValidMethodInfo(this.HorizontalEvent.GetPersistentTarget(i), this.HorizontalEvent.GetPersistentMethodName(i), new Type[] { typeof(float) });
    //        UnityAction<float> execute = (float f) => info.Invoke(this.HorizontalEvent.GetPersistentTarget(i), new object[] { f });
    //        newState.HorizontalEvent.AddListener(execute);
    //    }

    //    for (int i = 0; i < LookXEvent.GetPersistentEventCount(); i++)
    //    {
    //        MethodInfo info = UnityEventBase.GetValidMethodInfo(this.LookXEvent.GetPersistentTarget(i), this.LookXEvent.GetPersistentMethodName(i), new Type[] { typeof(float) });
    //        UnityAction<float> execute = (float f) => info.Invoke(this.LookXEvent.GetPersistentTarget(i), new object[] { f });
    //        newState.LookXEvent.AddListener(execute);
    //    }

    //    for (int i = 0; i < LookYEvent.GetPersistentEventCount(); i++)
    //    {
    //        MethodInfo info = UnityEventBase.GetValidMethodInfo(this.LookYEvent.GetPersistentTarget(i), this.LookYEvent.GetPersistentMethodName(i), new Type[] { typeof(float) });
    //        UnityAction<float> execute = (float f) => info.Invoke(this.LookYEvent.GetPersistentTarget(i), new object[] { f });
    //        newState.LookYEvent.AddListener(execute);
    //    }

    //    for (int i = 0; i < InteractionEvent.GetPersistentEventCount(); i++)
    //    {
    //        MethodInfo info = UnityEventBase.GetValidMethodInfo(this.InteractionEvent.GetPersistentTarget(i), this.InteractionEvent.GetPersistentMethodName(i), new Type[] {  });
    //        UnityAction execute = () => info.Invoke(this.InteractionEvent.GetPersistentTarget(i), new object[] {  });
    //        newState.InteractionEvent.AddListener(execute);
    //    }

    //    return newState;
    //}
}
