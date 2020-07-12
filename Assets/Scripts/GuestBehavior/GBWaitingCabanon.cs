using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GBWaitingCabanon : AbsGuestBehavior
{
    public override bool ShouldTakeKeys()
    {
        return GetComponent<GuestMain>().Drunkness > 0f;
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
