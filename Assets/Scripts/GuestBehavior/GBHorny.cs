using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Pathfinding;

public class GBHorny : AbsGuestBehavior
{
    public GBHorny partner;
    float timer;
    // Start is called before the first frame update
    void Start()
    {
        timer = Random.Range(15, 20);
        Ai = GetComponent<IAstarAI>();
        List<GBHorny> horny = GameObject.FindGameObjectsWithTag("Guest").ToList().Select(x => x.GetComponent<GBHorny>()).Where(x => x != null && x != this && x.partner == null).ToList();
        if (horny.Count > 0)
        {
            partner = horny[UnityEngine.Random.Range(0, horny.Count)];
            partner.GetComponent<GBHorny>().partner = this;
            Ai.destination = new Vector3(6.8f, 0.4f, 0);
            partner.Ai.destination = new Vector3(6.8f, 0.4f, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        if (timer < 0 && partner == null)
        {
            ChangeBehavior(typeof(GBIdle));
        }
        else if (Ai.reachedDestination)
        {
            ChangeBehavior(typeof(GBIdle));
        }
    }
}
