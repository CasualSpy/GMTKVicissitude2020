using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GBIdle : AbsGuestBehavior
{
    float TimerIdle;

    private void Start()
    {
        Ai = GetComponent<IAstarAI>();
        Ai.destination = SpotInsideHouse();

        TimerIdle = Random.Range(15, 30);
    }

    Vector2 SpotInsideHouse()
    {
        return new Vector2(Random.Range(-5, 2), Random.Range(-3, 2));
    }

    public void Update()
    {
        TimerIdle -= Time.deltaTime;

        
        if (TimerIdle < 0)
        {

            ChangeBehavior(typeof(GBLeave));
        } else if (Ai.reachedDestination)
        {
            if (Random.Range(1, 4) == 1)
                Ai.destination = SpotInsideHouse();
            else
                Ai.destination = Random.insideUnitSphere + transform.position;
        }
    }
}
