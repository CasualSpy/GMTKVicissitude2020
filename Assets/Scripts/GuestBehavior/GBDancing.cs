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

            //36%
            if (rnd < 0.2)
                ChangeBehavior(typeof(GBIdle));
            //28%
            else if (rnd < 0.4)
                ChangeBehavior(typeof(GBGetDrink));
            //20%
            else if (rnd < 0.6)
                ChangeBehavior(typeof(GBHorny));
            //12%
            else if (rnd < 0.8)
                ChangeBehavior(typeof(GBRaiseVolume));
            //4%
            else
                ChangeBehavior(typeof(GBSmokingDog));
        }
    }

    private void OnDestroy()
    {
        GetComponent<Animator>().SetBool("Dancing", false);
    }
}
