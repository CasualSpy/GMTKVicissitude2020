using Pathfinding;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuestMain : MonoBehaviour
{
    private IAstarAI Ai;

    // Start is called before the first frame update

    private SpriteRenderer spriteRenderer;
    private int drunkness;
    public const int maxDrunkness = 3;

    private bool isGrabbed;

    public bool IsGrabbed
    {
        get { return isGrabbed; }
        set {
            isGrabbed = value;
            Ai.canMove = !isGrabbed;
        }
    }


    public int Drunkness
    {
        get => drunkness; set {
            drunkness = value;
            spriteRenderer.color = new Color(1-(float)drunkness / maxDrunkness, 1, 1-(float)drunkness / maxDrunkness);

        }
    }

    //public Transform transform;

    enum State
    {
        Idle,
        Leaving
    }

    void Start()
    {
        Ai = GetComponent<IAstarAI>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        ChangeBehavior(typeof(GBIdle));
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ChangeBehavior(Type type)
    {
        AbsGuestBehavior Oldbehavior = GetComponent<AbsGuestBehavior>();
        if (Oldbehavior != null)
            Destroy(Oldbehavior);
        gameObject.AddComponent(type);
    }



}
