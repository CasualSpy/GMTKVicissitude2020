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

    // Start is called before the first frame update
    void Start()
    {
        GFX = transform.Find("GFX");
        Ai = GetComponent<IAstarAI>();
        Puke = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Puke.prefab", typeof(GameObject));
        Ai.destination = Helpers.SpotInsideHouse();
        Ai.maxSpeed = 0.5f;
        //Puke = (GameObject)Resources.Load("Prefabs/Puke", typeof(GameObject));
    }

    private void OnDestroy()
    {
        Ai.maxSpeed = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        PukeTimer -= Time.deltaTime;

        if (PukeTimer < 0)
        {
            //BLERGH
            isPuking = true;
            GFX.localPosition = Vector3.zero;
            Instantiate(Puke, transform.position, Quaternion.identity);
            GetComponent<GuestMain>().Drunkness = 0;
            ChangeBehavior(typeof(GBIdle));
        }

        ShakeTimer -= Time.deltaTime;
        if (ShakeTimer <= 0f && !isPuking)
        {
            ShakeTimer = 0.05f;
            GFX.localPosition = Random.insideUnitCircle / 4;
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
            isPuking = true;
            GFX.localPosition = Vector3.zero;
            //Instantiate(Puke, transform.position, Quaternion.identity);
            GetComponent<GuestMain>().Drunkness = 0;
            ChangeBehavior(typeof(GBIdle));
        }
    }
}
