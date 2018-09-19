using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CameraController))]
public class CameraEditor : Editor {

    CameraController cameraController;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        using (var check = new EditorGUI.ChangeCheckScope())
        { 
            if(check.changed)
            {
                cameraController.UpdateFOV();
            }
        }
    }

    private void OnEnable()
    {
        cameraController = (CameraController)target;
    }
}
