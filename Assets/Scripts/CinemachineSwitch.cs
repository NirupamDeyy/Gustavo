using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemachineSwitch : MonoBehaviour
{
    private Animator animator;

    private bool menuCam = true;
    [SerializeField]
    private CinemachineVirtualCamera Vcam1;

    [SerializeField]
    private CinemachineFreeLook Vcam2;

    public void SwitchPriority()
    {
        if(menuCam)
        {
            Debug.Log("okay");
            Vcam1.Priority = 0;
            Vcam2.Priority = 1;
            Vcam2.gameObject.SetActive(true);
        }
        else
        {
            Vcam1.Priority = 1;
            Vcam2.Priority = 0;
            Vcam2.gameObject.SetActive(false);
        }
        menuCam = !menuCam;
    }

}
