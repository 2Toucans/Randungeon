using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public float maxSpeed = 5;
    public float movementAccel = 10f;
    public float turnAccel = 360f;
    public float maxTurnSpeed = 180f;
    public float jumpImpulse = 4f;

    private float turnSpeed = 0;
    private bool noclip = false;

    private Rigidbody body;

    // Use this for initialization
    void Start () {
	    body = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        if (Input.GetButtonDown("Jump") && !noclip) {
            Jump();
        }
        else if (Input.GetButton("Jump") && noclip) {
            MoveUp();
        }
        if (Input.GetButton("Crouch") && noclip) {
            MoveDown();
        }
        if (Input.GetAxis("Vertical") > 0.4f) {
            MoveForward();
        }
        else if (Input.GetAxis("Vertical") < -0.4f) {
            MoveBackward();
        }
        if (Input.GetAxis("Horizontal") < -0.4f) {
            TurnLeft();
        }
        else if (Input.GetAxis("Horizontal") > 0.4f) {
            TurnRight();
        }
        else {
            if (turnSpeed > 0) {
                turnSpeed -= turnAccel * 3 * Time.fixedDeltaTime;
                if (turnSpeed < 0) {
                    turnSpeed = 0;
                }
            }
            else if (turnSpeed < 0) {
                turnSpeed += turnAccel * 3 * Time.fixedDeltaTime;
                if (turnSpeed > 0) {
                    turnSpeed = 0;
                }
            }
        }

        // Miscellaneous Buttons
        if (Input.GetButtonDown("NoClip")) {
            ToggleNoclip();
        }

        CapSpeed();
        transform.Rotate(new Vector3(0, turnSpeed * Time.fixedDeltaTime, 0));
    }

    private void Jump() {
        if (Physics.Raycast(transform.position, -transform.up, 1.25f)) {
            body.velocity += transform.up * jumpImpulse;
        }
    }

    private void MoveForward() {
        body.velocity += transform.forward * movementAccel * Time.fixedDeltaTime;
    }

    private void MoveBackward() {
        body.velocity += transform.forward * -1 * movementAccel * Time.fixedDeltaTime;
    }

    private void TurnLeft() {
        turnSpeed -= turnAccel * Time.fixedDeltaTime;
        if (turnSpeed < -maxTurnSpeed) {
            turnSpeed = -maxTurnSpeed;
        }
    }

    private void TurnRight() {
        turnSpeed += turnAccel * Time.fixedDeltaTime;
        if (turnSpeed > maxTurnSpeed) {
            turnSpeed = maxTurnSpeed;
        }
    }

    private void MoveUp() {
        body.velocity += transform.up * movementAccel * Time.fixedDeltaTime;
    }

    private void MoveDown() {
        body.velocity += transform.up * -1 * movementAccel * Time.fixedDeltaTime;
    }

    private void CapSpeed() {
        if (Vector3.Scale(body.velocity, new Vector3(1, 0, 1)).sqrMagnitude > maxSpeed * maxSpeed) {
            Vector3 vel = body.velocity.normalized * maxSpeed;
            body.velocity = Vector3.Scale(body.velocity, Vector3.up)
                + Vector3.Scale(vel, new Vector3(1, 0, 1));
        }
    }

    private void ToggleNoclip() {
        noclip = !noclip;
        Collider c = GetComponent<Collider>();
        c.enabled = !noclip;
        body.useGravity = !noclip;
    }
}
