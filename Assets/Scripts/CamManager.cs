using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamManager : MonoBehaviour
{
    public static CamManager current;

    public CinemachineVirtualCamera focusCamera;
    public CinemachineVirtualCamera deathCamera;
    public CinemachineVirtualCamera transitionCam;

    private void Awake()
    {
        current = this;
    }
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void FocusCamera(Transform follow, Transform lookAt)
    {
        focusCamera.Priority = 11;
        focusCamera.LookAt = lookAt;
        focusCamera.Follow = follow;
    }

    public void UnfocusCamera()
    {
        focusCamera.Priority = 9;
    }

    public void FocusOnDeathCam()
    {
        deathCamera.Priority = 12;
    }

    public void UnfocusOnDeathCam()
    {
        deathCamera.Priority = 9;
    }

    public void FocusOnTransitionCam()
    {
        transitionCam.Priority = 13;
    }

    public void UnfocusOnTransitionCam()
    {
        transitionCam.Priority = 9;
    }

}
