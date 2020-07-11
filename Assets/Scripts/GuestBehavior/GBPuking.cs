using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GBPuking : AbsGuestBehavior
{
    GameObject Puke;

    float PukeTimer = 30;

    float ShakeTimer = 0.1f;
    Transform GFX;
    bool isPuking = false;
    bool isInToilet = false;

    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        GFX = transform.Find("GFX");
        Ai = GetComponent<IAstarAI>();
        Puke = Resources.Load("Puke") as GameObject;
        Ai.destination = Helpers.SpotToHangOut();
        Ai.maxSpeed = 0.5f;

        animator = GetComponent<Animator>();
        animator.SetBool("About to puke", true);
        //Puke = (GameObject)Resources.Load("Prefabs/Puke", typeof(GameObject));
    }

    private void OnDestroy()
    {
        Ai.maxSpeed = 1f;
        Ai.canMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        PukeTimer -= Time.deltaTime;

        if (PukeTimer < 0 && !isPuking)
        {
            //BLERGH
            GameObject.Find("Master").GetComponent<ComplaintManager>().AddComplaint(new ComplaintManager.Complaint(GetComponent<GuestMain>(), ComplaintManager.Reasons.Puke));
            isPuking = true;
            Instantiate(Puke, transform.position, Quaternion.identity);
            Ai.canMove = false;
            animator.SetBool("Puking", true);
            Invoke("FinishPuking", 3f);

        }

        //ShakeTimer -= Time.deltaTime;
        //if (ShakeTimer <= 0f && !isPuking)
        //{
        //    ShakeTimer = 0.05f;
        //    GFX.localPosition = Random.insideUnitCircle / 4;
        //}

        if (Ai.reachedDestination && !isPuking)
        {
                Ai.destination = Random.insideUnitSphere + transform.position;
        }


    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!isPuking && !isInToilet && collision.name == "Restroom")
        {
            //Is in toilet
            //BLERGH
            
            isPuking = true;
            GameObject.Find("Player").GetComponent<ControlsManager>().Release();
            transform.position = GameObject.Find("Toilet").transform.position;// - new Vector3(0,0.3f);
            Ai.canMove = false;
            animator.SetBool("Puking", true);
            Invoke("FinishPuking", 3f);
        }
    }

    void FinishPuking()
    {
        animator.SetBool("About to puke", false);
        animator.SetBool("Puking", false);
        GetComponent<GuestMain>().Drunkness -= 2;
        ChangeBehavior(typeof(GBIdle));
        Ai.canMove = true;
    }
}
