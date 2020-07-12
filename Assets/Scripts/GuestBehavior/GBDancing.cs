using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GBDancing : AbsGuestBehavior
{

    float timer;

    // Start is called before the first frame update
    void Start()
    {
        timer = Random.Range(15, 30);
        GetComponent<Animator>().SetBool("Dancing", true);
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            float rnd = Helpers.RandomRebelliousChoice(GetComponent<GuestMain>());
            bool isDriver = GetComponent<GuestMain>().isDriver;

            //Avg: 36% Driver: (51%)
            if (rnd < 0.2 || rnd < 0.3 && isDriver)
                ChangeBehavior(typeof(GBDancing));
            //Avg: 28% Driver: (13%)
            else if (rnd < 0.4)
                ChangeBehavior(typeof(GBGetDrink));
            //Avg: 20% Driver: (11%)
            else if (rnd < 0.6 || rnd < 0.5 && isDriver)
                ChangeBehavior(typeof(GBHorny));
            //Avg: 12% Driver: (16%)
            else if (rnd < 0.8 || rnd < 0.7 && isDriver)
                ChangeBehavior(typeof(GBRaiseVolume));
            //Avg: 4% Driver: (9%)
            else
                ChangeBehavior(typeof(GBLeave));
        }
    }

    private void OnDestroy()
    {
        GetComponent<Animator>().SetBool("Dancing", false);
    }
}
