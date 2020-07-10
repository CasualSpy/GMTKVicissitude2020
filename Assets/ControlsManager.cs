using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlsManager : MonoBehaviour
{
    private Rigidbody2D rb2d;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        float movementVertical = Input.GetAxis("Vertical");
        float movementHorizontal = Input.GetAxis("Horizontal");

        Vector2 movement = new Vector2(movementHorizontal, movementVertical);
        rb2d.AddForce(movement * speed);
    }
}
