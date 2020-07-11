using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GBDrink : AbsGuestBehavior
{

    private void Start()
    {
        Ai = GetComponent<IAstarAI>();
        Ai.destination = GameObject.Find("Bar").transform.position;

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.transform.name == "Bar")
        {
            //Bar has been touched! Let's drink!
            GuestMain guestMain = GetComponent<GuestMain>();
            guestMain.Drunkness++;

            if (guestMain.Drunkness == GuestMain.maxDrunkness)
                ChangeBehavior(typeof(GBPuking));
            else
                ChangeBehavior(typeof(GBIdle));
        }
    }

    public void Update()
    {
        //TimerIdle -= Time.deltaTime;


        //if (TimerIdle < 0)
        //{

        //    ChangeBehavior(typeof(GBLeave));
        //}
        //else if (Ai.reachedDestination)
        //{
        //    if (Random.Range(1, 4) == 1)
        //        Ai.destination = SpotInsideHouse();
        //    else
        //        Ai.destination = Random.insideUnitSphere + transform.position;
        //}
    }
}
