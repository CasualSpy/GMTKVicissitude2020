using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GBDrinking : AbsGuestBehavior
{

    float TimerDrinking;
    Transform GFX;

    private void Start()
    {
        Ai = GetComponent<IAstarAI>();
        Ai.destination = Helpers.SpotToHangOut();
        GFX = transform.Find("GFX");

        TimerDrinking = Random.Range(15, 30);
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
            guestMain.Drunkness++;
            GFX.localPosition = Vector3.zero;

            if (guestMain.Drunkness == GuestMain.maxDrunkness)
                ChangeBehavior(typeof(GBPuking));
            else
                ChangeBehavior(typeof(GBIdle));
        }


        GFX.localPosition = new Vector3(0, Mathf.Sin(Time.timeSinceLevelLoad * 8) / 4);

        if (Ai.reachedDestination)
        {
            if (Random.Range(1, 4) == 1)
                Ai.destination = Helpers.SpotToHangOut();
            else
                Ai.destination = Random.insideUnitSphere + transform.position;
        }
    }
}
