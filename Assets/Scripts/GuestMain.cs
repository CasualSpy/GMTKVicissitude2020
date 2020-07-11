using Pathfinding;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuestMain : MonoBehaviour
{
    // Start is called before the first frame update


    public int yo;
    //public Transform transform;

    enum State
    {
        Idle,
        Leaving
    }

    void Start()
    {

        ChangeBehavior(typeof(GBIdle));
        yo = 5;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ChangeBehavior(Type type)
    {
        AbsGuestBehavior Oldbehavior = GetComponent<AbsGuestBehavior>();
        if (Oldbehavior != null)
            Destroy(Oldbehavior);
        gameObject.AddComponent(type);
    }

}
