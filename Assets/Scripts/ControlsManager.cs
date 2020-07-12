using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlsManager : MonoBehaviour
{
    private Rigidbody2D rb2d;
    SpriteRenderer spriteRenderer;
    public float speed;
    bool isGrabbing = false;
    GameObject targetGuest = null;
    GameObject targetObject = null;

    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        AnimatorManage();

        targetGuest = null;
        targetObject = null;

        foreach (var item in Physics2D.OverlapCircleAll(transform.position, 0.2f))
        {
            if (targetGuest == null && item.tag == "Guest")
            {
                //Found guest
                targetGuest = item.gameObject;
                targetGuest.GetComponent<GuestMain>().isHighlighted = true;
                break;
            }
            if (targetObject == null && item.tag == "Object")
            {
                targetObject = item.gameObject;
                targetObject.GetComponent<ObjectMain>().isHighlighted = true;
                break;
            }
        }


        //Grab guest
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrabbing)
            {
                Release();
            }
            else
            {
                if (targetGuest != null)
                {
                    //Grab'Em!
                    FixedJoint2D joint = gameObject.AddComponent<FixedJoint2D>();
                    joint.connectedBody = targetGuest.GetComponent<Rigidbody2D>();
                    isGrabbing = true;
                    targetGuest.GetComponent<GuestMain>().IsGrabbed = true;
                }
            }
        }

        //Kick guest
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (targetGuest != null)
                targetGuest.GetComponent<GuestMain>().ChangeBehavior(typeof(GBKicked));
        }

        //Interactions with objects
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (targetGuest != null)
                targetGuest.GetComponent<GuestMain>().TakeKeys();
            if (targetObject != null)
            {
                if (targetObject.name == "Jukebox")
                    GameObject.Find("Master").GetComponent<SoundLevel>().Lower();
                if (targetObject.name == "CondomPile")
                    GameObject.Find("Player").GetComponent<CondomController>().HasCondom = true;
            }
        }
    }

    public void Release()
    {
        FixedJoint2D joint = GetComponent<FixedJoint2D>();
        joint.connectedBody.GetComponent<GuestMain>().IsGrabbed = false;
        joint.connectedBody.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        Destroy(joint);
        isGrabbing = false;
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        float movementVertical = Input.GetAxisRaw("Vertical");
        float movementHorizontal = Input.GetAxisRaw("Horizontal");


        Vector2 movement = new Vector2(movementHorizontal, movementVertical);


        //if (movement.magnitude != 0)
        //rb2d.velocity = rb2d.velocity.magnitude * movement.normalized;

        rb2d.AddForce(movement.normalized * speed);
    }

    void AnimatorManage()
    {
        animator.SetBool("Walking", (rb2d.velocity.magnitude > 0.2f) ||
            Input.GetAxisRaw("Vertical") != 0 ||
            Input.GetAxisRaw("Horizontal") != 0
            );
        animator.SetBool("Grabbing", isGrabbing);


        if (isGrabbing)
        {
            spriteRenderer.flipX = (transform.position - targetGuest.transform.position).x > 0;
        }
        else
        {
            if (rb2d.velocity.x < -0.1f)
                spriteRenderer.flipX = false;
            else if (rb2d.velocity.x > 0.1f)
                spriteRenderer.flipX = true;
        }
    }
}
