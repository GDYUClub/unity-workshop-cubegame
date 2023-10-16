using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float moveSpeed;
    public Transform[] waypoints;

    private int currentPoint;

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, waypoints[currentPoint].position, moveSpeed * Time.deltaTime); // Move towards current waypoint

        if (transform.position == waypoints[currentPoint].position) // Waypoint reached
        {
            if (currentPoint == waypoints.Length - 1) // Last waypoint reached
            {
                currentPoint = 0;
            }
            else
            {
                currentPoint++;
            }
        }
    }
}
