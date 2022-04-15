using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class CarController : MonoBehaviour
{
    private Rigidbody2D carRb;
    [SerializeField] private int force;
    private int fwMode = 1;
    
    void Start()
    {
        carRb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            Debug.Log("Throtte");
            Throtte();
        }
        if (Input.GetKey(KeyCode.C))
            Brake();
        if (Input.GetKey(KeyCode.R))
            fwMode = -fwMode;
    }

    public void Throtte()
    {
        carRb.AddForce(transform.right * force * fwMode, ForceMode2D.Force);
    }

    public void Brake()
    {
        if (carRb.velocity.magnitude > 0)
            carRb.AddForce(-carRb.velocity.normalized * force, ForceMode2D.Force);
    }
}
