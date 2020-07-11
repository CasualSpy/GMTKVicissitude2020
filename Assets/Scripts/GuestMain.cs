using Pathfinding;
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
    private int drunkness;
    public const int maxDrunkness = 2;
    public bool isDriver;
    public bool hasKeys;
    public bool isMinor;
    public bool isHighlighted;
    public Sprite driverSprite;
    public Sprite minorSprite;
    //How likely they are to do stupid stuff
    public float rebelliousness;
    public GBHorny partner;
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

        if (Ai.velocity.x < -0.1f)
            spriteRenderer.flipX = true;
        else if (Ai.velocity.x > 0.1f)
            spriteRenderer.flipX = false;



    }
}
