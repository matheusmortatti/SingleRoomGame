using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastEntity : MonoBehaviour {

    public RaycastStateParams[] raycastParams;

    private RaycastStateParams currentParam;

    private int layerMask = 0;

    private RaycastHit[] results;
    private RaycastHit lastHit;
    private bool hasHitLastFrame = false, hasHitNow = false;
    private InteractionStateHandler interactionStateHandler;

    void Start()
    {
        interactionStateHandler = FindObjectOfType<InteractionStateHandler>();

        if(interactionStateHandler == null)
        {
            Debug.LogError("There's no InteractionStateHandler Instantiated!");
        }

        currentParam = FindRaycastParams();
    }

    void Update()
    {
        CastRay();
    }

    private void CastRay()
    {
        RaycastStateParams newParams = FindRaycastParams();
        if (newParams == null)
            return;
        if (newParams != currentParam)
        {
            hasHitLastFrame = false;
            lastHit = new RaycastHit();
        }

        currentParam = newParams;

        layerMask = CreateLayer(currentParam.layersToRaycast);

        Vector3 origin = transform.position;
        Vector3 direction = transform.forward;
        Ray ray = new Ray(origin, direction);
        results = Physics.RaycastAll(ray, currentParam.rayLength, layerMask);

        Debug.DrawRay(origin, direction* currentParam.rayLength, Color.red);

        TreatRaycastHit(results);
    }

    private void TreatRaycastHit(RaycastHit[] results)
    {
        bool isValidTag = false;

        float dist = float.MaxValue;
        RaycastHit result = new RaycastHit();
        foreach(RaycastHit h in results)
        {
            if(Maths.IsAny(h.transform.tag, currentParam.tagsToLookFor))
            {
                isValidTag = true;
                if(h.distance < dist)
                {
                    result = h;
                    dist = h.distance;
                }
            }
        }

        if (isValidTag)
        {
            RaycastEvent objectEvents = result.transform.GetComponent<RaycastEvent>();
            if (!hasHitLastFrame)
            {
                objectEvents.InvokeEnter();
                Debug.Log("Enter");
            }
            else
            {
                objectEvents.InvokeStay();
                Debug.Log("Stay");
            }

            if (lastHit.transform != null && result.transform.gameObject != lastHit.transform.gameObject)
            {
                RaycastEvent lastObjectEvents = lastHit.transform.GetComponent<RaycastEvent>();
                lastObjectEvents.InvokeExit();
                Debug.Log("Exit " + lastHit.transform.name);
            }

            lastHit = result;
        }
        else if(hasHitLastFrame)
        {
            RaycastEvent lastObjectEvents = lastHit.transform.GetComponent<RaycastEvent>();
            lastObjectEvents.InvokeExit();
            Debug.Log("Exit " + lastHit.transform.name);
        }

        hasHitLastFrame = isValidTag;
    }

    private int CreateLayer(string[] layers)
    {
        int layerMask = 0;
        // Create layer mask
        foreach (string l in layers)
        {
            layerMask = layerMask | 1 << LayerMask.NameToLayer(l);
        }

        return layerMask;
    }

    private RaycastStateParams FindRaycastParams()
    {
        RaycastStateParams rsp = null;

        foreach(RaycastStateParams p in raycastParams)
        {
            if(p.state == interactionStateHandler.GetCurrentState())
            {
                rsp = p;
                break;
            }
        }

        return rsp;
    }
}

[System.Serializable]
public class RaycastStateParams
{
    public InteractionStateNames state;

    public float rayLength = 3;
    public string[] layersToRaycast;
    public string[] tagsToLookFor;
}
