using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GBIdle : AbsGuestBehavior
{
    float TimerIdle;
    GuestMain gm;
    float ChillingInPlace = 0;

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
        ChillingInPlace -= Time.deltaTime;
        if (TimerIdle < 0)
        {
            float rnd = Helpers.RandomRebelliousChoice(gm);
            bool isDriver = GetComponent<GuestMain>().isDriver;

            if (rnd < 0.2 || rnd < 0.3 && isDriver)
                ChangeBehavior(typeof(GBDancing));
            else if (rnd < 0.4)
                ChangeBehavior(typeof(GBGetDrink));
            else if (rnd < 0.6 || rnd < 0.5 && isDriver)
                ChangeBehavior(typeof(GBHorny));
            else if (rnd < 0.8 || rnd < 0.7 && isDriver)
                ChangeBehavior(typeof(GBRaiseVolume));
            else
                ChangeBehavior(typeof(GBLeave));
        }
        else if (Ai.reachedDestination && ChillingInPlace < 0)
        {
            if (Random.Range(1, 2) == 1)
            {
                //Chill in place
                ChillingInPlace = Random.Range(5, 7);
            }
            else
            {
                if (Random.Range(1, 6) == 1)
                    Ai.destination = Helpers.SpotToHangOut();
                else
                    Ai.destination = Random.insideUnitSphere + transform.position;
            }
        }
    }

    public override bool ShouldTakeKeys()
    {
        return GetComponent<GuestMain>().Drunkness > 0f;
    }
}
