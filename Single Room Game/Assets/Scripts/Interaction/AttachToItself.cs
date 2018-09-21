using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachToItself : MonoBehaviour {

    [SerializeField]
    private GameObject attachedObject;

    public bool IsAttached()
    {
        return attachedObject != null;
    }

    public GameObject accessAttachedObject()
    {
        return attachedObject;
    }

    public bool Attach(GameObject obj)
    {
        if (attachedObject != null || obj == null) return false;

        attachedObject = obj;

        Rigidbody rb = attachedObject.GetComponent<Rigidbody>();
        if(rb != null)
        {
            rb.useGravity = false;
        }

        return true;
    }

    public void ForceAttach(GameObject obj)
    {
        Detach();

        attachedObject = obj;

        Rigidbody rb = attachedObject.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.useGravity = false;
        }
    }

    public void Detach()
    {
        if (attachedObject == null) return;

        Rigidbody rb = attachedObject.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.useGravity = true;
        }

        attachedObject = null;
    }

    public bool DetachIfEquals(GameObject obj)
    {
        if (attachedObject == null || attachedObject != obj) return false;

        Rigidbody rb = attachedObject.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.useGravity = true;
        }

        attachedObject = null;

        return true;
    }

    void FixedUpdate()
    {
        if(attachedObject != null)
        {
            Rigidbody rb = attachedObject.GetComponent<Rigidbody>();
            
            if(rb != null)
            {
                rb.MovePosition(this.transform.position);
            }
            else
            {
                attachedObject.transform.position = this.transform.position;
            }
            attachedObject.transform.rotation = this.transform.rotation;
        }
    }
	
}
