using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class CarController : MonoBehaviour
{
    private Rigidbody2D carRb;
    [SerializeField] private int force;
    private int fwMode = 1;

    public float steer = 0;
    public SteeringWheel SteeringWheel;
    
    void Start()
    {
        carRb = GetComponent<Rigidbody2D>();
        CanvasManager.SharedInstance.SetGearImage(fwMode);
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            Throtte();
        }
        if (Input.GetKey(KeyCode.C))
            Brake();
        if (Input.GetKeyDown(KeyCode.R))
        {
            fwMode = -fwMode;
            CanvasManager.SharedInstance.SetGearImage(fwMode);
        }

        KillOrthoVelocity();

        Steer();
    }

    public void Throtte()
    {
        carRb.MoveRotation(steer);
        carRb.AddForce(transform.right * force * fwMode, ForceMode2D.Force);
    }

    public void Steer()
    {
        //steer += Input.GetAxis("Horizontal") * 90 * Time.deltaTime * (carRb.velocity.magnitude * 0.1f);
        steer = SteeringWheel.Angle * (carRb.velocity.magnitude * 0.1f);
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

        carRb.velocity = forwardVel + othorVel * 0.94f;
    }    
}
