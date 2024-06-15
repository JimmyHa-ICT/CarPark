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
    public int fwMode = 1;
    
    [SerializeField] public float steer = 0;
    public float Velocity => carRb.velocity.magnitude;
    [HideInInspector] public PositionLogger PositionLogger;
   

    void Start()
    {
        carRb = GetComponent<Rigidbody2D>();
        PositionLogger = GetComponent<PositionLogger>();
        //CanvasManager.SharedInstance.SetGearImage(fwMode);
        Init();
    }

    private void FixedUpdate()
    {
        if (!photonView.IsMine)
        {
            return;
        }
        KillOrthoVelocity();
    }

    public void Init()
    {
        steer = transform.eulerAngles.z;
        carRb.simulated = false;
        carRb.velocity = Vector2.zero;
        carRb.angularVelocity = 0;
        carRb.simulated = true;
    }

    public void Throtte()
    {
        carRb.AddForce(transform.right * force * fwMode, ForceMode2D.Force);
    }

    public void Steer(float angle)
    {
        //steer += Mathf.Lerp(-1, 1, (angle + 3) / 6) * angularVelocity * Time.deltaTime * (carRb.velocity.magnitude * 0.1f);
        //Debug.Log("Steer");
        steer += Mathf.Lerp(-1, 1, (angle + 3) / 6) * angularVelocity * Time.deltaTime * (carRb.velocity.magnitude * 0.1f);
        carRb.MoveRotation(steer);
    }

    public void Brake()
    {
        if (carRb.velocity.magnitude > 0)
            carRb.AddForce(-carRb.velocity.normalized * force * 2f, ForceMode2D.Force);
    }

    private void KillOrthoVelocity()
    {
        Vector2 forwardVel = transform.right * Vector2.Dot(carRb.velocity, transform.right);
        Vector2 othorVel = transform.up * Vector2.Dot(carRb.velocity, transform.up);

        carRb.velocity = forwardVel + othorVel * 0.9f;
    }    
}
