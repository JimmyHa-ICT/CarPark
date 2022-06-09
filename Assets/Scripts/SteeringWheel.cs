using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SteeringWheel : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    Vector3 oldDir;
    Camera mainCam;
    public float SteerInput = 0;
    [SerializeField] private float currentAngle;

    [SerializeField] private readonly float DAMPING = 100;
    [SerializeField] private readonly float BOUND = 300;
    private float damping;



    [HideInInspector] public float Angle
    {
        get
        {
            return transform.eulerAngles.z;
        }
    }
    
    private void Start()
    {

        mainCam = Camera.main;
        SteerInput = 0;
        currentAngle = 0;
        damping = DAMPING;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 mousePos = mainCam.ScreenToWorldPoint(eventData.position);
        Vector3 dir = new Vector3(mousePos.x - transform.position.x,
                                    mousePos.y - transform.position.y,
                                    0);
        float angle = Vector3.SignedAngle(oldDir, dir, Vector3.forward);

        currentAngle = Mathf.Clamp(currentAngle + angle, -BOUND, BOUND);
        transform.eulerAngles = new Vector3(0, 0, currentAngle);
        oldDir = dir;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        damping = 0;
        Vector3 mousePos = mainCam.ScreenToWorldPoint(eventData.position);
        oldDir = new Vector3(mousePos.x - transform.position.x,
                                    mousePos.y - transform.position.y,
                                    0);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        damping = DAMPING;
    }

    private void Update()
    {
        currentAngle = Mathf.MoveTowards(currentAngle, 0, damping * Time.deltaTime);
        transform.eulerAngles = new Vector3(0, 0, currentAngle);
        SteerInput = currentAngle / BOUND;
        if (MobileInputSender.Instance != null)
            MobileInputSender.Instance.Steering = SteerInput;
    }
}
