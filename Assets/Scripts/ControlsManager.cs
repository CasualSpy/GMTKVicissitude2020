using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlsManager : MonoBehaviour
{
    private Rigidbody2D rb2d;
    public float speed;
    bool isGrabbing = false;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        GameObject targetGuest = null;
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


        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrabbing)
            {
                FixedJoint2D joint = GetComponent<FixedJoint2D>();
                joint.connectedBody.GetComponent<GuestMain>().IsGrabbed = false;
                Destroy(joint);
                isGrabbing = false;
            } else
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
                targetGuest.GetComponent<GuestMain>().ChangeBehavior(typeof(GBLeave));
            }
        }
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
}
