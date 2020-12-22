using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMovement : MonoBehaviour
{
    //config params
    [SerializeField] float horizontalSpeed = 1f;
    [SerializeField] float verticalSpeed = 1f;
    [SerializeField] float maxXVelocity = 10f;
    [SerializeField] float maxYVelocity = 10f;
    [SerializeField] float dampenStrength = .9f;
    [SerializeField] float dampenThreshold = .5f;

    //references
    Rigidbody2D shipRigidBody;

    //state var

    // Start is called before the first frame update
    void Start()
    {
        shipRigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
        UpdateThrust();
        ClampVelocity();
       
        if (ShipIsMoving())
        {
            DampenVelocity();
        }
    }

    public void SetAcceleration(float speedFactor)
    {
        horizontalSpeed = speedFactor * 1000;
        verticalSpeed = horizontalSpeed / 2;
    }

    public void SetTopSpeed(float speedFactor)
    {
        maxXVelocity = speedFactor * 2;
        maxYVelocity = speedFactor;
    }

    public void SetDampenStrength(float strength)
    {
        dampenStrength = strength;
    }
    
    public void SetShipMass(float mass)
    {
        gameObject.GetComponent<Rigidbody2D>().mass = mass * 5;
    }

    private bool ShipIsMoving()
    {
        if (shipRigidBody.velocity.magnitude > dampenThreshold)
        {
            return true;
        }
        else
        {
            return false;
        }

    }

    private void DampenVelocity()
    {
        float newX = shipRigidBody.velocity.x;
        float newY = shipRigidBody.velocity.y;
        if(Input.GetAxis("Horizontal") == 0)
        {
            newX *= dampenStrength;
        }
        if(Input.GetAxis("Vertical") == 0)
        {
            newY *= dampenStrength;
        }

        shipRigidBody.velocity = new Vector2(newX, newY);

    }

    private bool MoveInput()
    {
        bool isMoving = false;
        Debug.Log(Input.GetAxis("Horizontal") + " " + Input.GetAxis("Vertical"));
        if(Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            isMoving = true;
        }

        return isMoving;
    }


    private void UpdateThrust()
    {
        float xAxis = Input.GetAxis("Horizontal");
        float yAxis = Input.GetAxis("Vertical");

        float xForce = xAxis * horizontalSpeed;
        float yForce = yAxis * verticalSpeed;

        Vector2 force = new Vector2(xForce, yForce);

        shipRigidBody.AddForce(force);

        
    }

    private void ClampVelocity() 
    {

        float maxX = Mathf.Clamp(shipRigidBody.velocity.x, -maxXVelocity, maxXVelocity);
        float maxY = Mathf.Clamp(shipRigidBody.velocity.y, -maxYVelocity, maxYVelocity);

        Vector2 clampedVelocity = new Vector2(maxX, maxY);


        shipRigidBody.velocity = clampedVelocity;
    }
}
