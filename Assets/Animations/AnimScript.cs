using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AnimScript : MonoBehaviour
{
    public Transform[] waypoints;
    public GameObject player;
    private NavMeshAgent agent;
    private Renderer renderer;
    private int currentWaypoint;
    private AudioSource audio;
    private int timeSincePlayed;
    
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        renderer = GetComponent<Renderer>();
        audio = GetComponent<AudioSource>();
        currentWaypoint = 0;
        timeSincePlayed = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(waypoints[currentWaypoint].position, transform.position) < 3f) {
            if (currentWaypoint == waypoints.Length - 1) {
                currentWaypoint = 0;
            } else {
                currentWaypoint++;
            }
        }
        agent.SetDestination(waypoints[currentWaypoint].position);
        timeSincePlayed++;
        if (Vector3.Distance(player.transform.position, transform.position) < 2f) {
            if (timeSincePlayed > 200) {
                audio.Play();
                timeSincePlayed = 0;
            }
        }
    }
}
