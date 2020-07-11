using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GBPuking : AbsGuestBehavior
{
    GameObject Puke;

    float PukeTimer = 30;

    bool isPuking = false;
    bool isInToilet = false;

    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        Ai = GetComponent<IAstarAI>();
        Puke = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Puke.prefab", typeof(GameObject));
        Ai.destination = Helpers.SpotToHangOut();
        Ai.maxSpeed = 0.5f;

        animator = GetComponent<Animator>();
        animator.SetBool("About to puke", true);
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
            isPuking = true;
            Instantiate(Puke, transform.position, Quaternion.identity);
            Ai.canMove = false;
            animator.SetBool("Puking", true);
            Invoke("FinishPuking", 3f);

        }

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
            transform.position = GameObject.Find("Toilet").transform.position;
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
