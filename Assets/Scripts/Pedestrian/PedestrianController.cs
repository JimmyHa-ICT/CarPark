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

        if ((transform.position - waypoints[currentWaypoint].position).sqrMagnitude < 0.001f)
        {
            currentWaypoint = (currentWaypoint + 1) % waypoints.Length;
        }
        //transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(waypoints[currentWaypoint].position - transform.position), 360 * Time.deltaTime);
        //transform.position += -transform.up * velocity * Time.deltaTime;
        Vector3 direction = waypoints[currentWaypoint].position - transform.position;
        transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90);
        transform.position = Vector3.MoveTowards(transform.position, waypoints[currentWaypoint].position, velocity * Time.deltaTime);
    }
}
