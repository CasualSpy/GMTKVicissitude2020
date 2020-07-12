﻿using Pathfinding;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GuestMain : MonoBehaviour, Plaintiff
{
    private IAstarAI Ai;

    private Rigidbody2D rb2d;
    private SpriteRenderer spriteRenderer;

    private SpriteRenderer keySpriteRenderer;

    private int drunkness;
    public const int maxDrunkness = 4;
    public bool isDriver;
    private bool hasKeys;
    public bool isMinor;
    public bool isHighlighted;
    public Sprite driverSprite;
    public Sprite minorSprite;
    //How likely they are to do stupid stuff
    public float rebelliousness;
    public GuestMain partner;
    private GuestSpawner spawner;

    private Animator animator;

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
        get;
        set;
    }
    public bool HasKeys { get => hasKeys; set {
            if (!keySpriteRenderer)
                keySpriteRenderer = transform.Find("Key").GetComponent<SpriteRenderer>();
            keySpriteRenderer.enabled = value;
            hasKeys = value; 
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
        keySpriteRenderer = transform.Find("Key").GetComponent<SpriteRenderer>();

        spawner = GameObject.Find("Master").GetComponent<GuestSpawner>();
        spawner.GuestCount++;
        Ai = GetComponent<IAstarAI>();
        rb2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        rebelliousness = Helpers.CalcRebellion();
        if (isMinor)
            spriteRenderer.sprite = minorSprite;
        else if (isDriver)
            spriteRenderer.sprite = driverSprite;
        ChangeBehavior(typeof(GBIdle));


        animator = GetComponent<Animator>();
    }

    public void TakeKeys()
    {
        if (isDriver && HasKeys)
        {
            AbsGuestBehavior gb = GetComponent<AbsGuestBehavior>();
            if (!gb.ShouldTakeKeys())
            {
                GameObject.Find("Master").GetComponent<ComplaintManager>().AddComplaint(new ComplaintManager.Complaint(this, ComplaintManager.Reasons.Designated_driver_keys_taken_early));
            }
            HasKeys = false;
        } else
        {
            Debug.Log("Take keys failed: isDriver = " + isDriver.ToString() + "; hasKeys = " + HasKeys.ToString());
        }
    }

    public void MakeDriver()
    {
        isDriver = true;
        HasKeys = true;
    }

    public void MakeMinor()
    {
        isMinor = true;
    }

    // Update is called once per frame
    void Update()
    {
        AnimatorManager();


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

    private void OnDestroy()
    {
        spawner.GuestCount--;
    }

    public void Complain(ComplaintManager.Reasons reason)
    {
        throw new NotImplementedException();
    }

    public void AnimatorManager()
    {
        
        animator.SetBool("Walking", Ai.velocity.magnitude > 0.7f);



        if (Ai.velocity.x < 0.1f)
            spriteRenderer.flipX = true;
        else if (Ai.velocity.x > 0.1f)
            spriteRenderer.flipX = false;



    }
}
