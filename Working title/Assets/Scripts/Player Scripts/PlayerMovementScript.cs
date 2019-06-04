using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{
    [SerializeField] int AccelSpeed, Jumpforce, MaxSpeed;

    Rigidbody p_RB;
    // Start is called before the first frame update
    void Start()
    {
        p_RB = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (p_RB.velocity.x<MaxSpeed&&p_RB.velocity.z<MaxSpeed)
        { 
            if (Mathf.Abs(Input.GetAxis("Vertical")) > 0)
            {
                p_RB.AddForce(this.transform.forward * Input.GetAxis("Vertical") * AccelSpeed);
            }
            if (Mathf.Abs(Input.GetAxis("Horizontal")) > 0)
            {
                p_RB.AddForce(this.transform.right * Input.GetAxis("Horizontal") * AccelSpeed);
            }
        }
        if(Mathf.Abs(p_RB.velocity.x)>MaxSpeed)
        {
            p_RB.velocity = new Vector3(MaxSpeed * Input.GetAxis("Horizontal"), p_RB.velocity.y, p_RB.velocity.z);
        }
        if (Mathf.Abs(p_RB.velocity.z) > MaxSpeed)
        {
            p_RB.velocity = new Vector3(p_RB.velocity.x, p_RB.velocity.y, MaxSpeed * Input.GetAxis("Vertical"));
        }

    }
}
