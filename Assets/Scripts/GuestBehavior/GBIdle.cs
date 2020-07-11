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
        Ai.destination = Helpers.SpotToHangOut();

        TimerIdle = Random.Range(15, 30);
    }



    public void Update()
    {
        TimerIdle -= Time.deltaTime;

        
        if (TimerIdle < 0)
        {

            ChangeBehavior(typeof(GBGetDrink));
        } else if (Ai.reachedDestination)
        {
            if (Random.Range(1, 6) == 1)
                Ai.destination = Helpers.SpotToHangOut();
            else
                Ai.destination = Random.insideUnitSphere + transform.position;
        }
    }
}
