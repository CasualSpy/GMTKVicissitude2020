using System.Collections;
using Pathfinding;
using System.Collections.Generic;
using UnityEngine;

public class GBLeave : AbsGuestBehavior
{
    float TimerIdle;

    private void Start()
    {
        Ai = GetComponent<IAstarAI>();
        Ai.destination = new Vector3(-11, 0, 0);
    }

    public void Update()
    {
        if (transform.position.x < -10)
        {
            Destroy(gameObject);
        }
    }

}