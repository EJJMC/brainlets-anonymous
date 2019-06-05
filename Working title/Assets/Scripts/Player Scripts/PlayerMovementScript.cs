using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{
    [SerializeField] int AccelSpeed, Jumpforce, MaxSpeed;


    [SerializeField] Transform raycastPos;
    RaycastHit raycastHit;
    float groundAngle;
    bool grounded;

    Vector3 CustomForward;

    Rigidbody p_RB;
    // Start is called before the first frame update
    void Start()
    {
        p_RB = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        GroundRaycast();
        GroundAngleReader();
        NewForward();
        Movement();

        Debug.Log(groundAngle);
        
    }

    void Movement()
    {
        
        if (Mathf.Abs(Input.GetAxis("Vertical")) > 0)
        {
            p_RB.AddForce(CustomForward * Input.GetAxis("Vertical") * AccelSpeed);
        }
        if (Mathf.Abs(Input.GetAxis("Horizontal")) > 0)
        {
            p_RB.AddForce(this.transform.right * Input.GetAxis("Horizontal") * AccelSpeed);
        }

        
       

        // this is the speed limiter for the player.
        if (p_RB.velocity.magnitude > MaxSpeed)
        {
            p_RB.velocity = p_RB.velocity.normalized * MaxSpeed;
        }



        if (Input.GetKeyDown(KeyCode.Space))
        {
            p_RB.AddForce(new Vector3(0, Jumpforce, 0));
        }
    }

    void NewForward()
    {
        if(!grounded)
        {
            CustomForward = transform.forward;
            return;
        }

        CustomForward = Vector3.Cross(transform.right, raycastHit.normal);

    }


    void GroundAngleReader()
    {
        if(!grounded)
        {
            groundAngle = 0f;
            return;
        }

        groundAngle = Vector3.Angle(raycastHit.normal, transform.forward);
    }

    void GroundRaycast()
    {
        if(Physics.Raycast(transform.position, -Vector3.up, out raycastHit))
        {
            grounded = true;
        }
        else
        {
            grounded = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
       
        Gizmos.DrawLine(this.transform.position, CustomForward.normalized*5+this.transform.position);
    }
}
