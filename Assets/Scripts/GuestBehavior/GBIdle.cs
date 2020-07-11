using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GBIdle : AbsGuestBehavior
{
    float TimerIdle;
    GuestMain gm;

    private void Start()
    {
        gm = gameObject.GetComponent<GuestMain>();
        Ai = GetComponent<IAstarAI>();
        Ai.destination = Helpers.SpotToHangOut();

        TimerIdle = Random.Range(15, 30);
    }



    public void Update()
    {
        TimerIdle -= Time.deltaTime;
        if (TimerIdle < 0)
        {
            float rnd = Helpers.RandomRebelliousChoice(gm);

            //36%
            if (rnd < 0.2)
                ChangeBehavior(typeof(GBDancing));
            //28%
            else if (rnd < 0.4)
                ChangeBehavior(typeof(GBGetDrink));
            //20%
            else if (rnd < 0.6)
                ChangeBehavior(typeof(GBHorny));
            //12%
            else if (rnd < 0.8)
                ChangeBehavior(typeof(GBRaiseVolume));
            //4%
            else
                ChangeBehavior(typeof(GBSmokingDog));
        } else if (Ai.reachedDestination)
        {
            if (Random.Range(1, 6) == 1)
                Ai.destination = Helpers.SpotToHangOut();
            else
                Ai.destination = Random.insideUnitSphere + transform.position;
        }
    }
}
