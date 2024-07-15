using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PedestrianController : MonoBehaviourPun
{
    [SerializeField] private Transform[] waypoints;
    [SerializeField] private float velocity = 2;
    private int currentWaypoint;

    public float vel;

    private void Awake()
    {
        if (PhotonNetwork.PlayerList.Length > 1 && photonView.IsMine)
            Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        currentWaypoint = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (!photonView.IsMine)
            return;

        CheckObscuring();
        if ((transform.position - waypoints[currentWaypoint].position).sqrMagnitude < 0.001f)
        {
            currentWaypoint = (currentWaypoint + 1) % waypoints.Length;
        }
        //transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(waypoints[currentWaypoint].position - transform.position), 360 * Time.deltaTime);
        //transform.position += -transform.up * velocity * Time.deltaTime;
        vel = velocity;
        var collider = CheckObscuring().collider;
        if (CheckObscuring().collider != null)
        {
            Debug.Log(collider);
            var car = collider.GetComponent<CarController>();
            var pedestrian = collider.GetComponent<PedestrianController>();
            if (car != null && car.Velocity <= 0.1f)
            {
                vel = velocity;
            }
            else if (pedestrian != null && pedestrian.vel == 0)
            {
                vel = velocity;
            }
            else
                vel = 0;
        }
        Vector3 direction = waypoints[currentWaypoint].position - transform.position;
        transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90);
        transform.position = Vector3.MoveTowards(transform.position, waypoints[currentWaypoint].position, vel * Time.deltaTime);
    }

    private RaycastHit2D CheckObscuring()
    {
        Debug.DrawRay(transform.position + transform.right * 0.31f, -transform.up * .75f, Color.red);
        Debug.DrawRay(transform.position - transform.right * 0.31f, -transform.up * .75f, Color.red);
        var right = Physics2D.Raycast(transform.position + transform.right * 0.31f, -transform.up, .75f, LayerMask.GetMask("Cars", "Human"));
        var left = Physics2D.Raycast(transform.position - transform.right * 0.31f, -transform.up, .75f, LayerMask.GetMask("Cars", "Human"));

        if (right.collider == null && left.collider == null)
        {
            return right;
        }    
        else if (right.collider == null)
        {
            if (left.collider.transform == transform)
                return right;
            return left;
        }
        else if (left.collider == null)
        {
            if (right.collider.transform == transform)
                return left;
            return right;
        }
        else
        {
            Vector2 pos = transform.position;
            float rightDistance = Vector2.SqrMagnitude(right.point - pos);
            float leftDistance = Vector2.SqrMagnitude(left.point - pos);
            if (right.collider.transform == transform && left.collider.transform == transform)
                return new RaycastHit2D();
            else if (right.collider.transform == transform)
                return left;
            else if (left.collider.transform == transform)
                return right;

            return rightDistance < leftDistance ? right : left;
        }
    }    
}
