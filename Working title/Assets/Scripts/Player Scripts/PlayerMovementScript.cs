using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{
    [SerializeField] int  Jumpforce, MaxSpeed, MoveForce;
    int AccelSpeed;

    [SerializeField] int MidAirSpeed;

    [SerializeField] Transform raycastPos;
    RaycastHit raycastHit, LeftHit, RightHit;
    float groundAngle, LeftAngle, RightAngle;
    bool grounded, slopeLeft, slopeRight;

    Vector3 CustomForward, CustomRight, CustomLeft;

    Rigidbody p_RB;
    // Start is called before the first frame update
    void Start()
    {
        p_RB = this.GetComponent<Rigidbody>();
        AccelSpeed = MoveForce;
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
        if (Input.GetKey(KeyCode.Space)&&grounded)
        {
            p_RB.AddForce(new Vector3(0, Jumpforce, 0), ForceMode.Impulse);
        }

        if (grounded)
        {
            AccelSpeed = MoveForce;
        }
        else
        {
            AccelSpeed = MidAirSpeed;
        }

        if (Mathf.Abs(Input.GetAxis("Vertical")) > 0)
        {
            
                p_RB.AddForce(CustomForward * Input.GetAxis("Vertical") * AccelSpeed);
            
        }
        if (Mathf.Abs(Input.GetAxis("Horizontal")) > 0 )
        {
            
                p_RB.AddForce(transform.right * Input.GetAxis("Horizontal") * AccelSpeed);
           

        }

        // this is the speed limiter for the player.
        Vector3 XZVector = new Vector3(p_RB.velocity.x, 0,p_RB.velocity.z);

       if (XZVector.magnitude > MaxSpeed&&grounded)
        {
            p_RB.velocity = p_RB.velocity.normalized * MaxSpeed;
            
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

    void NewLeft()
    {
        if(!slopeLeft)
        {
            CustomLeft = -transform.right;
            return;
        }

        CustomLeft = Vector3.Cross(CustomForward, LeftHit.normal);
    }

    void NewRight()
    {
        if(!slopeRight)
        {
            CustomRight = transform.right;
            return;
        }
        CustomRight = Vector3.Cross(CustomForward, RightHit.normal);
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
        //raycast directly down.
        if(Physics.Raycast(transform.position, -Vector3.up, out raycastHit, 2f))
        {
            grounded = true;
        }
        else
        {
            grounded = false;
        }

        //raycast left of player
        if(Physics.Raycast(raycastPos.position, -transform.right, out LeftHit,1f))
        {
            slopeLeft = true;

            Debug.Log("Slope to Left");
        }
        else
        {
            slopeLeft = false;
        }

        //raycast right of player
        if (Physics.Raycast(raycastPos.position, transform.right, out RightHit, 1f))
        {
            slopeRight = true;
            Debug.Log("Slope to Right");
        }
        else
        {
            slopeRight = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
       
        Gizmos.DrawLine(this.transform.position, CustomForward.normalized*5+this.transform.position);

        Gizmos.DrawLine(raycastPos.position, CustomRight.normalized * 5 + raycastPos.position);
        Gizmos.DrawLine(raycastPos.position, CustomLeft.normalized * 5 + raycastPos.position);


    }
}
