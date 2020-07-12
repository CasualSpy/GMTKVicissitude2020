using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CabanonManager : MonoBehaviour
{
    GameObject Particles;
    public struct Couple
    {
        public GuestMain first;
        public GuestMain second;

        public Couple(GuestMain first, GuestMain second)
        {
            this.first = first;
            this.second = second;
        }
    }

    Queue<Couple> queue;
    bool occupied;
    Couple occupants;
    float timer;

    public bool protectionApplied = false;
    // Start is called before the first frame update
    void Start()
    {
        Particles = GameObject.Find("HornyParticles");
        Particles.SetActive(false);
        queue = new Queue<Couple>();
        occupied = false;
    }

    public void WaitInLine(Couple c)
    {
        queue.Enqueue(c);
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (occupied && timer < 0)
        {
            StopWoohoo();
        }
        if (queue.Count > 0 && !occupied)
        {
            Woohoo(queue.Dequeue());
        }
    }

    void Woohoo(Couple c)
    {
        protectionApplied = false;

        occupants = c;
        occupied = true;
        timer = Random.Range(10, 15);
        GameObject firstGuest = c.first.gameObject;
        GameObject secondGuest = c.second.gameObject;
        firstGuest.GetComponent<CircleCollider2D>().enabled = false;
        SpriteRenderer[] firstSrs = firstGuest.GetComponentsInChildren<SpriteRenderer>();
        foreach (var sr in firstSrs)
        {
            sr.enabled = false;
        }
        secondGuest.GetComponent<CircleCollider2D>().enabled = false;
        SpriteRenderer[] secondSrs = secondGuest.GetComponentsInChildren<SpriteRenderer>();
        foreach (var sr in secondSrs)
        {
            sr.enabled = false;
        }
        Particles.SetActive(true);
    }

    void StopWoohoo()
    {
        if (!protectionApplied)
        {
            GameObject.Find("Master").GetComponent<ComplaintManager>().AddComplaint(new ComplaintManager.Complaint(occupants.first.gameObject.GetComponent<GuestMain>(), ComplaintManager.Reasons.Unsafe_sex));
        }
        Particles.SetActive(false);

        GameObject firstGuest = occupants.first.gameObject;
        GameObject secondGuest = occupants.second.gameObject;

        firstGuest.GetComponent<CircleCollider2D>().enabled = true;
        secondGuest.GetComponent<CircleCollider2D>().enabled = true;

        GuestMain firstMain = firstGuest.GetComponent<GuestMain>();
        GuestMain secondMain = secondGuest.GetComponent<GuestMain>();

        SpriteRenderer[] firstSrs = firstGuest.GetComponentsInChildren<SpriteRenderer>();
        foreach (var sr in firstSrs)
        {
            ShowSprites(firstMain, sr);
        }
        secondGuest.GetComponent<CircleCollider2D>().enabled = false;
        SpriteRenderer[] secondSrs = secondGuest.GetComponentsInChildren<SpriteRenderer>();
        foreach (var sr in secondSrs)
        {
            ShowSprites(secondMain, sr);
        }
        firstMain.ChangeBehavior(typeof(GBIdle));
        secondMain.ChangeBehavior(typeof(GBIdle));


        occupied = false;
    }

    void ShowSprites(GuestMain guest, SpriteRenderer sr)
    {
            if (sr.gameObject.name == "Key")
                sr.enabled = guest.HasKeys;
            else
                sr.enabled = true;
    }
}
