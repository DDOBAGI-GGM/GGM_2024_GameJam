using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private Camera _cam;
    public Camera cam
    {
        get
        {
            if (_cam == null)
                _cam = Camera.main;
            return _cam;
        }
    }

    private void Update()
    {
        CamAngleChange();
    }

    private void CamAngleChange()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {

        }
    }
}
