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


        foreach (var item in Physics2D.OverlapCircleAll(transform.position, 0.2f))
        {
            if (item.tag == "Guest")
            {
                //Found guest
                targetGuest = item.gameObject;
                targetGuest.GetComponent<GuestMain>().isHighlighted = true;
                break;
            }
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            if (targetGuest != null)
                targetGuest.GetComponent<GuestMain>().TakeKeys();
        }

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
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (targetGuest != null)
            {
                targetGuest.GetComponent<GuestMain>().ChangeBehavior(typeof(GBKicked));
            }
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            foreach (var item in Physics2D.OverlapCircleAll(transform.position, 0.2f))
            {
                if (item.name == "Jukebox")
                {
                    GameObject.Find("Master").GetComponent<SoundLevel>().Lower();
                }
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
