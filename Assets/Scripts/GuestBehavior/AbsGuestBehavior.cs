using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System;

public abstract class AbsGuestBehavior : MonoBehaviour
{

    protected GuestMain _guestMain;
    protected IAstarAI Ai;

    public virtual bool ShouldTakeKeys()
    {
        return GetComponent<GuestMain>().Drunkness > 0f;
    }

    public void ChangeBehavior(Type type)
    {
        AbsGuestBehavior Oldbehavior = GetComponent<AbsGuestBehavior>();
        if (Oldbehavior != null)
            Destroy(Oldbehavior);
        gameObject.AddComponent(type);
    }
}
