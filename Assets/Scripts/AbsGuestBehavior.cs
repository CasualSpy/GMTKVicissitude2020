using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public abstract class AbsGuestBehavior : MonoBehaviour
{

    protected GuestMain _guestMain;
    protected IAstarAI Ai;
}
