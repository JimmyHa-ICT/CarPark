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

    public SteeringWheel wheel => UiController.Instance.UIIngame.Wheel;
    public ThrottleButton throttleButton => UiController.Instance.UIIngame.ThrottleButton;
    public BrakeButton brakeButton => UiController.Instance.UIIngame.BrakeButton;
    public GearButton gearButton => UiController.Instance.UIIngame.GearButton;

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
            throttleButton.OnUpdate(PlayerInput.Instance.ThrottlePressed);
            brakeButton.OnUpdate(PlayerInput.Instance.BrakePressed);
        }
        else
        {
            if (Input.GetKey(KeyCode.Space))
            {
                carController.Throtte();
            }
            if (Input.GetKey(KeyCode.C))
                carController.Brake();
            throttleButton.OnUpdate(Input.GetKey(KeyCode.Space));
            brakeButton.OnUpdate(Input.GetKey(KeyCode.C));
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
            wheel.OnUpdate(PlayerInput.Instance.WheelInput);

            carController.fwMode = PlayerInput.Instance.Gear;
            gearButton.OnUpdate(carController.fwMode);
        }
        else
        {
            carController.Steer(Input.GetAxis("Horizontal") * 3);
            wheel.OnUpdate(Input.GetAxis("Horizontal"));
            if (Input.GetKeyDown(KeyCode.R))
            {
                carController.fwMode = -carController.fwMode;
                gearButton.OnUpdate(carController.fwMode);
                //CanvasManager.SharedInstance.SetGearImage(fwMode);
            }
        }

    }
}
