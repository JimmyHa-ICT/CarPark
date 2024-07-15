using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CarInput : MonoBehaviourPun
{
    [SerializeField] private CarController carController;
    public bool isMobileMode;
    private bool isThrottle = false;
    private bool isBrake = false;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!photonView.IsMine)
        {
            return;
        }

        if (isMobileMode)
        {
            if (PlayerInput.Instance.ThrottlePressed)
            {
                Debug.Log("Throttle");
                carController.Throtte();
            }
            if (PlayerInput.Instance.BrakePressed)
            {
                carController.Brake();
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.Space))
            {
                carController.Throtte();
            }
            if (Input.GetKey(KeyCode.C))
                carController.Brake();
        }
    }

    private void Update()
    {
        if (!photonView.IsMine)
        {
            return;
        }

        if (isMobileMode)
        {
            carController.Steer(PlayerInput.Instance.WheelInput * 3);

            carController.fwMode = PlayerInput.Instance.Gear;
        }
        else
        {
            carController.Steer(Input.GetAxis("Horizontal") * 3);
            if (Input.GetKeyDown(KeyCode.R))
            {
                carController.fwMode = -carController.fwMode;
                //CanvasManager.SharedInstance.SetGearImage(fwMode);
            }
        }

    }
}
