using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GBRaiseVolume : AbsGuestBehavior
{

    private void Start()
    {
        Ai = GetComponent<IAstarAI>();
        Ai.destination = GameObject.Find("Jukebox").transform.position;

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.transform.name == "Jukebox")
        {
            GameObject.Find("Master").GetComponent<SoundLevel>().Increase();
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
