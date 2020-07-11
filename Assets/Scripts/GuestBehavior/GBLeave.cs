using System.Collections;
using Pathfinding;
using System.Collections.Generic;
using UnityEngine;

public class GBKicked : AbsGuestBehavior
{
    float TimerIdle;

    private void Start()
    {
        Ai = GetComponent<IAstarAI>();
        Ai.destination = new Vector3(-11, 0, 0);
    }

    public void Update()
    {
        if (transform.position.x < -10)
        {
            GuestMain guest = gameObject.GetComponent<GuestMain>();

            if (guest.isMinor)
            {
                GameObject.Find("Master").GetComponent<ComplaintManager>().AddComplaint(new ComplaintManager.Complaint());
            }

            Destroy(gameObject);
        }
    }

}