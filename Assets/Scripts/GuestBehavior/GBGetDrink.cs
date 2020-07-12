using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GBGetDrink : AbsGuestBehavior
{

    private void Start()
    {
        Ai = GetComponent<IAstarAI>();
        Ai.destination = GameObject.Find("Bar").transform.position;
        Bounds bounds = GameObject.Find("Bar").GetComponent<BoxCollider2D>().bounds;
        Ai.destination = new Vector3(
        Random.Range(bounds.min.x, bounds.max.x),
        Random.Range(bounds.min.y, bounds.max.y));

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.transform.name == "Bar")
        {
            //Bar has been touched! Let's drink!
            ChangeBehavior(typeof(GBDrinking));
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

    public override bool ShouldTakeKeys()
    {
        return GetComponent<GuestMain>().Drunkness > 0f;
    }
}
