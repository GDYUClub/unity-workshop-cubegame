using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Inputs playerInputs;
    private Rigidbody rb;

    public float maxVelocity;
    public float moveForce;

    // Start is called before the first frame update
    void Start()
    {
        playerInputs = new Inputs();
        playerInputs.Enable();
        rb = GetComponent<Rigidbody>();
    }

    // FixedUpdate is called 50 times per sec
    void FixedUpdate() 
    {
        Vector2 moveInput = playerInputs.Player.Movement.ReadValue<Vector2>();
        Vector3 moveVect = new Vector3(moveInput.x, 0, moveInput.y);

        if (rb.velocity.magnitude < maxVelocity)
        {
            rb.AddForce(moveVect * moveForce);
        }
    }
}
