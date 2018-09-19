using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraController : MonoBehaviour {

    public float normalFOV, zoomFOV;
    public float lerpVelocity = 0.05f;
    private Camera camera;
    private float axisLerpValue = 0;

    void Start()
    {
        camera = GetComponent<Camera>();
        UpdateFOV();
    }

    public void ProcessZoomIn(float val)
    {
        Debug.Log(val);
        axisLerpValue = Maths.Lerp(axisLerpValue, val, lerpVelocity);
        camera.fieldOfView = Mathf.Lerp(normalFOV, zoomFOV, axisLerpValue);
    }

    public void UpdateFOV()
    {
        camera.fieldOfView = normalFOV;
    }
	
}
