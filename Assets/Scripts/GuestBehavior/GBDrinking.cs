using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GBDrinking : AbsGuestBehavior
{

    float TimerDrinking;
    GameObject drink;

    private void Start()
    {
        drink = Resources.Load("drinkPrefab") as GameObject;

        Ai = GetComponent<IAstarAI>();
        Ai.destination = Helpers.SpotToHangOut();

        TimerDrinking = Random.Range(15, 30);

        GetComponent<Animator>().SetBool("Drinking", true);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {

    }

    public void Update()
    {

        TimerDrinking -= Time.deltaTime;
        if (TimerDrinking < 0)
        {
            SpawnDrink();
            GuestMain guestMain = GetComponent<GuestMain>();
            guestMain.Drunkness += 2;

            if (guestMain.Drunkness == GuestMain.maxDrunkness)
                ChangeBehavior(typeof(GBPuking));
            else
                ChangeBehavior(typeof(GBIdle));
        }

        if (Ai.reachedDestination)
        {
            if (Random.Range(1, 4) == 1)
                Ai.destination = Helpers.SpotToHangOut();
            else
                Ai.destination = Random.insideUnitSphere + transform.position;
        }
    }

    private void OnDestroy()
    {
        GetComponent<Animator>().SetBool("Drinking", false);

    }

    private void SpawnDrink()
    {
        Quaternion quaternion = Quaternion.identity;
        switch (Random.Range(0, 3))
        {
            case 0:
                quaternion = Quaternion.Euler(0, 0, 0);
                break;
            case 1:
                quaternion = Quaternion.Euler(0, 0, 90);
                break;
            case 2:
                quaternion = Quaternion.Euler(0, -0, -90);
                break;
            default:
                break;
        }

        Instantiate(drink, transform.position, quaternion);
    }

    public override bool ShouldTakeKeys()
    {
        return true;
    }
}
