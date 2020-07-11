using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GBDrinking : AbsGuestBehavior
{

    float TimerDrinking;

    private void Start()
    {
        Ai = GetComponent<IAstarAI>();
        Ai.destination = Helpers.SpotToHangOut();

        TimerDrinking = Random.Range(15, 30);

        GetComponent<Animator>().SetBool("Drinking", true);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {

    }

    public void Update()
    {
        TimerDrinking -= Time.deltaTime;
        if (TimerDrinking < 0)
        {
            GuestMain guestMain = GetComponent<GuestMain>();
            guestMain.Drunkness += 2;

            if (guestMain.Drunkness == GuestMain.maxDrunkness)
                ChangeBehavior(typeof(GBPuking));
            else
                ChangeBehavior(typeof(GBIdle));
        }

        if (Ai.reachedDestination)
        {
            if (Random.Range(1, 4) == 1)
                Ai.destination = Helpers.SpotToHangOut();
            else
                Ai.destination = Random.insideUnitSphere + transform.position;
        }
    }

    private void OnDestroy()
    {
        GetComponent<Animator>().SetBool("Drinking", false);
    }
}
