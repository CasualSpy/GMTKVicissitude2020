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
                //This pretty much picks up one of the closer guest when space is pressed. Needs to be changed to mouse click (or keep it like that?)
                GameObject guest = null;
                foreach (var item in Physics2D.OverlapCircleAll(transform.position, 0.2f))
                {
                    if (item.tag == "Guest")
                    {
                        //Found guest
                        guest = item.gameObject;
                        break;
                    }
                } 

                if (guest != null)
                {
                    //Grab'Em!
                    FixedJoint2D joint = gameObject.AddComponent<FixedJoint2D>();
                    joint.connectedBody = guest.GetComponent<Rigidbody2D>();
                    isGrabbing = true;
                    guest.GetComponent<GuestMain>().IsGrabbed = true;
                }
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
