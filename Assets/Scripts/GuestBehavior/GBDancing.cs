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

            if (isDriver && gameObject.GetComponent<GuestMain>().Drunkness > 0 && Random.value < 0.25)
                ChangeBehavior(typeof(GBLeave));
            else if (rnd < 0.2 || rnd < 0.1 && isDriver)
                ChangeBehavior(typeof(GBGetDrink));
            else if (rnd < 0.4)
                ChangeBehavior(typeof(GBIdle));
            else if (rnd < 0.6)
                ChangeBehavior(typeof(GBHorny));
            else if (rnd < 0.8)
                ChangeBehavior(typeof(GBRaiseVolume));
            else
                ChangeBehavior(typeof(GBLeave));
        }
    }

    private void OnDestroy()
    {
        GetComponent<Animator>().SetBool("Dancing", false);
    }
}
