using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Pathfinding;

public class GBHorny : AbsGuestBehavior
{
    public float timerNoPartner;
    public float timerCantReach;
    public bool nearCabanon = false;
    GuestMain gm;
    // Start is called before the first frame update
    void Start()
    {
        gm = gameObject.GetComponent<GuestMain>();
        timerNoPartner = Random.Range(15, 20);
        Ai = GetComponent<IAstarAI>();
        List<GBHorny> horny = GameObject.FindGameObjectsWithTag("Guest").ToList().Select(x => x.GetComponent<GBHorny>()).Where(x => x != null && x != this && x.gm.partner == null).ToList();
        if (horny.Count > 0)
        {
            gm.partner = horny[UnityEngine.Random.Range(0, horny.Count)];
            gm.partner.GetComponent<GuestMain>().partner = this;
            timerCantReach = Random.Range(15, 20);
            gm.partner.timerCantReach = timerCantReach;
            Ai.destination = FrontOfCabanon();
            gm.partner.Ai.destination = FrontOfCabanon();
        }
    }

    // Update is called once per frame
    void Update()
    {
        timerNoPartner -= Time.deltaTime;
        if (timerNoPartner < 0 && gm.partner == null)
        {
            ChangeBehavior(typeof(GBIdle));
        }
        else if (gm.partner != null && timerCantReach >= 0)
            timerCantReach -= Time.deltaTime;
        else if (gm.partner != null && timerCantReach < 0)
        {
            gm.partner.GetComponent<GuestMain>().partner = null;
            gm.partner = null;

        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.transform.name == "Cabanon")
        {
            nearCabanon = true;
            if (gm.partner != null)
            {
                gm.partner.ChangeBehavior(typeof(GBWaitingCabanon));
                gm.ChangeBehavior(typeof(GBWaitingCabanon));
                GameObject.Find("Cabanon").GetComponent<CabanonManager>().WaitInLine(new CabanonManager.Couple(gm, gm.partner.GetComponent<GuestMain>()));
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        nearCabanon = false;
    }

    Vector3 FrontOfCabanon()
    {

        Collider2D collider = GameObject.Find("Cabanon").GetComponent<Collider2D>();
        var bounds = collider.bounds;
        var center = bounds.center;

        float x;
        float y;
        int attempt = 0;
        do
        {
            x = UnityEngine.Random.Range(center.x - bounds.extents.x, center.x + bounds.extents.x);
            y = UnityEngine.Random.Range(center.y - bounds.extents.y, center.y + bounds.extents.y);
            attempt++;
        } while (!collider.OverlapPoint(new Vector2(x, y)) || attempt >= 100);
        Debug.Log("Attemps: " + attempt);

        return new Vector3(x, y, 0);
    }
}
