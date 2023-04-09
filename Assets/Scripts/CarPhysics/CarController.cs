using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

[RequireComponent(typeof(Rigidbody2D))]
public class CarController : MonoBehaviourPun
{
    private Rigidbody2D carRb;
    [SerializeField] private int force;
    [SerializeField] private float angularVelocity;
    private int fwMode = 1;

    public float steer = 0;

    [HideInInspector] public PositionLogger PositionLogger;
   

    void Start()
    {
        carRb = GetComponent<Rigidbody2D>();
        PositionLogger = GetComponent<PositionLogger>();
        //CanvasManager.SharedInstance.SetGearImage(fwMode);
        steer = transform.eulerAngles.z;
    }

    private void FixedUpdate()
    {
        if (!photonView.IsMine)
        {
            return;
        }

        if (Input.GetKey(KeyCode.Space))
        {
            Throtte();
        }
        if (Input.GetKey(KeyCode.C))
            Brake();

        //if (MobileInputSender.Instance == null)
        //    return;

        //if (MobileInputSender.Instance.Throttle == 1)
        //{
        //    Throtte();
        //}
        //if (MobileInputSender.Instance.Brake == 1)
        //{
        //    Brake();
        //}

        //fwMode = MobileInputSender.Instance.Gear;

        KillOrthoVelocity();

        Steer();

        //if (carRb.velocity.magnitude > 0 && Time.frameCount % 10 == 0)
        //    PositionLogger.LogPosition();
    }

    private void Update()
    {
        if (!photonView.IsMine)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            fwMode = -fwMode;
            //CanvasManager.SharedInstance.SetGearImage(fwMode);
        }
    }

    public void Throtte()
    {
        carRb.MoveRotation(steer);
        carRb.AddForce(transform.right * force * fwMode, ForceMode2D.Force);
    }

    public void Steer()
    {
        steer += Input.GetAxis("Horizontal") * angularVelocity * Time.deltaTime * (carRb.velocity.magnitude * 0.1f);
        //steer += MobileInputSender.Instance.Steering * angularVelocity * Time.deltaTime * (carRb.velocity.magnitude * 0.1f);
        carRb.MoveRotation(steer);
    }    

    public void Brake()
    {
        if (carRb.velocity.magnitude > 0)
            carRb.AddForce(-carRb.velocity.normalized * force, ForceMode2D.Force);
    }

    private void KillOrthoVelocity()
    {
        Vector2 forwardVel = transform.right * Vector2.Dot(carRb.velocity, transform.right);
        Vector2 othorVel = transform.up * Vector2.Dot(carRb.velocity, transform.up);

        carRb.velocity = forwardVel + othorVel * 0.9f;
    }    
}
