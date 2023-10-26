using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class weapon_zoom : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;
    [SerializeField] float zoomedOutFOV = 40f;
    [SerializeField] float zoomedInFOV = 20f;
    [SerializeField] Animator WeaponAnimator;
    [SerializeField] Image imageComponent;
    bool zoomedInToggle = false;

    





    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (zoomedInToggle == false)
            {
                imageComponent.enabled = false;
                ZoomIn();
                WeaponAnimator.SetBool("zoomIn", true);

            }
            else
            {
                imageComponent.enabled = true;
                ZoomOut();
                WeaponAnimator.SetBool("zoomIn", false);

            }
        }
    }

    private void ZoomIn()
    {
        zoomedInToggle = true;
        virtualCamera.m_Lens.FieldOfView = zoomedInFOV;
    }

    private void ZoomOut()
    {
        zoomedInToggle = false;
        virtualCamera.m_Lens.FieldOfView = zoomedOutFOV;
    }
}
