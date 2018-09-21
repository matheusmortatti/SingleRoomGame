using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRotator : MonoBehaviour {

    public float rotateSensitivity = 1.0f;
    private Vector3 rotation;
    private Rigidbody rb;
    private Quaternion initialRotation;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        initialRotation = this.transform.rotation;
    }
	
	// Update is called once per frame
	void Update () {
        PerformRotation();
	}

    void PerformRotation()
    {
        if (rb != null)
        {
            rb.MoveRotation(rb.rotation * Quaternion.Euler(rotation));
        }
        else
        {
            this.transform.Rotate(rotation);
        }

        rotation = Vector3.zero;
    }

    public void ApplyRotation(Vector3 _rotation)
    {
        rotation = _rotation * rotateSensitivity;
    }

    public void ApplyRotationZ(float zRot)
    {
        rotation.z = zRot * rotateSensitivity;
    }

    public void ApplyRotationY(float yRot)
    {
        rotation.y = yRot * rotateSensitivity;
    }

    public void ApplyRotationX(float xRot)
    {
        rotation.x = xRot * rotateSensitivity;
    }

    public void ResetRotation()
    {
        if (rb != null)
        {
            rb.MoveRotation(initialRotation);
        }
        else
        {
            this.transform.rotation = initialRotation;
        }
    }
}
