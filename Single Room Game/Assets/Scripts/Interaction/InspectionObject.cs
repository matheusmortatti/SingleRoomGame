using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
public class InspectionObject : MonoBehaviour, IInteractable, IPhysicsObject
{

    public float throwVelocity = 2;

    private AttachToItself playerHand;
    private Vector3 InitialPosition;
    private Quaternion InitialRotation;
    private Rigidbody rb;
    private InteractionStateHandler interactionStateHandler;
    private PlayerController playerController;

    // Use this for initialization
    void Start () {
        GameObject playerHandObject = GameObject.FindGameObjectWithTag("PlayerHand");
        playerHand = playerHandObject.GetComponent<AttachToItself>();

        InitialPosition = this.transform.position;
        InitialRotation = this.transform.rotation;

        rb = GetComponent<Rigidbody>();
        interactionStateHandler = FindObjectOfType<InteractionStateHandler>();

        playerController = FindObjectOfType<PlayerController>();
    }

    public void SendInteractionMessage(string message)
    {
        playerController.InteractionMessage(message);
    }

    public void SetPickupButtonEvent()
    {
        playerController.RemoveAllAltListenersInteract();
        playerController.SetAltListenerInteract(new UnityAction(Interact));
    }

    public void SetPlaceBackButtonEvent()
    {
        playerController.RemoveAllAltListenersInteract();
        playerController.SetAltListenerInteract(new UnityAction(PlaceBack));
    }

    public void Interact()
    {
        if(playerHand != null)
        {
            if(playerHand.Attach(this.gameObject))
            {
                interactionStateHandler.ChangeState(InteractionStateNames.HOLDING);
            }
        }
    }

    public void Throw(Vector3 direction)
    {
        if (playerHand != null)
        {
            if(playerHand.DetachIfEquals(this.gameObject))
            {
                interactionStateHandler.ChangeState(InteractionStateNames.HANDSFREE);
            }
        }

        rb.velocity = direction * throwVelocity;
    }

    public void PlaceBack()
    {
        if(playerHand != null)
        {
            if (playerHand.DetachIfEquals(this.gameObject))
            {
                interactionStateHandler.ChangeState(InteractionStateNames.HANDSFREE);
            }
        }

        rb.MovePosition(InitialPosition);
        rb.MoveRotation(InitialRotation);
    }
}
