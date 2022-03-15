using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    // set speed limit
    [SerializeField] float horsePower = 0;
    [SerializeField] float turnSpeed = 45f;
    private Rigidbody playerRb;
    [SerializeField] GameObject centerOfMass;
    float horizontalInput;
    float verticalInput;
    [SerializeField] TextMeshProUGUI speedoMeterText;
    [SerializeField] TextMeshProUGUI rpmText;
    [SerializeField] float speed;
    [SerializeField] float rpmSpeed;

    [SerializeField] List<WheelCollider> allWheels;
    [SerializeField] int wheelsOnGround;



    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerRb.centerOfMass = centerOfMass.transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        if (isOnGround())
        {
            // Moves car on vertical input
            playerRb.AddRelativeForce(Vector3.forward * horsePower * verticalInput);
            // Rotates car on horizontal input
            transform.Rotate(Vector3.up, Time.deltaTime * turnSpeed * horizontalInput);

            speed = Mathf.Round(playerRb.velocity.magnitude * 2.237f);
            speedoMeterText.SetText("Speed= " + speed + " mph");

            rpmSpeed = Mathf.Round((speed % 30) * 40);
            rpmText.SetText("RPM = " + rpmSpeed);
        }



    }

    bool isOnGround()
    {
        wheelsOnGround = 0;
        foreach (WheelCollider wheel in allWheels)
        {
            if (wheel.isGrounded)
            {
                wheelsOnGround++;

            }
        }
        if (wheelsOnGround == 4)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
