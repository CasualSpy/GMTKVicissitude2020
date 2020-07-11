using Pathfinding;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GuestMain : MonoBehaviour
{
    private IAstarAI Ai;

    private SpriteRenderer spriteRenderer;
    private int drunkness;
    public const int maxDrunkness = 3;
    public bool isDriver;
    public bool hasKeys;
    public bool isMinor;
    public bool isHighlighted;
    public Sprite driverSprite;
    public Sprite minorSprite;
    //How likely they are to do stupid stuff
    public float rebelliousness;

    private bool isGrabbed;

    public bool IsGrabbed
    {
        get { return isGrabbed; }
        set
        {
            isGrabbed = value;
            Ai.canMove = !isGrabbed;
        }
    }


    public int Drunkness
    {
        get => drunkness; set
        {
            drunkness = value;
            spriteRenderer.color = new Color(1 - (float)drunkness / maxDrunkness, 1, 1 - (float)drunkness / maxDrunkness);

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
        rebelliousness = Helpers.CalcRebellion();
        if (isMinor)
            spriteRenderer.sprite = minorSprite;
        else if (isDriver)
            spriteRenderer.sprite = driverSprite;
        ChangeBehavior(typeof(GBIdle));
    }

    public void MakeDriver()
    {
        isDriver = true;
        hasKeys = true;
    }

    public void MakeMinor()
    {
        isMinor = true;
    }

    // Update is called once per frame
    void Update()
    {

        if (isHighlighted)
        {
            spriteRenderer.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
            isHighlighted = false;
        }
        else
        {
            GetComponentInChildren<SpriteRenderer>().transform.localScale = new Vector3(1, 1, 1);
        }
    }

    public void ChangeBehavior(Type type)
    {
        AbsGuestBehavior Oldbehavior = GetComponent<AbsGuestBehavior>();
        if (Oldbehavior != null)
            Destroy(Oldbehavior);
        gameObject.AddComponent(type);
    }



}
