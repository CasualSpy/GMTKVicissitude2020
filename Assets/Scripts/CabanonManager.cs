using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CabanonManager : MonoBehaviour
{
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
    // Start is called before the first frame update
    void Start()
    {
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
        occupants = c;
        occupied = true;
        timer = Random.Range(10, 15);
        GameObject firstGuest = c.first.gameObject;
        GameObject secondGuest = c.second.gameObject;
        firstGuest.GetComponent<CircleCollider2D>().enabled = false;
        firstGuest.GetComponentInChildren<SpriteRenderer>().enabled = false;
        secondGuest.GetComponent<CircleCollider2D>().enabled = false;
        secondGuest.GetComponentInChildren<SpriteRenderer>().enabled = false;
    }

    void StopWoohoo()
    {
        GameObject firstGuest = occupants.first.gameObject;
        GameObject secondGuest = occupants.second.gameObject;

        firstGuest.GetComponent<CircleCollider2D>().enabled = true;
        firstGuest.GetComponentInChildren<SpriteRenderer>().enabled = true;
        secondGuest.GetComponent<CircleCollider2D>().enabled = true;
        secondGuest.GetComponentInChildren<SpriteRenderer>().enabled = true;
        firstGuest.GetComponent<GuestMain>().ChangeBehavior(typeof(GBIdle));
        secondGuest.GetComponent<GuestMain>().ChangeBehavior(typeof(GBIdle));


        occupied = false;
    }
}
