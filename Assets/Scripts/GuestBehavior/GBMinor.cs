using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GBMinor : AbsGuestBehavior
{
    float TimerIdle;
    GuestMain gm;
    float ChillingInPlace = 0;
    bool isLeaving = false;

    private void Start()
    {
        gm = gameObject.GetComponent<GuestMain>();
        Ai = GetComponent<IAstarAI>();
        Ai.destination = Helpers.SpotToHangOut();

        TimerIdle = Random.Range(45, 75);
    }



    public void Update()
    {
        TimerIdle -= Time.deltaTime;
        ChillingInPlace -= Time.deltaTime;
        if (TimerIdle < 0)
        {
            if (!isLeaving)
                Ai.destination = new Vector3(-11, 0, 0);


            if (transform.position.x < -10)
            {
                GuestMain guest = gameObject.GetComponent<GuestMain>();
                GameObject.Find("Master").GetComponent<ComplaintManager>().AddComplaint(new ComplaintManager.Complaint(guest, ComplaintManager.Reasons.Minor));
            }
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
}
