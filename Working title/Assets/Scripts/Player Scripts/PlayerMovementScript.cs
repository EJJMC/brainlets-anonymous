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
        if (Mathf.Abs(p_RB.velocity.x)<MaxSpeed && Mathf.Abs(p_RB.velocity.z) < MaxSpeed)
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
       
        
        if(Input.GetKey(KeyCode.Space))
        {
            p_RB.AddForce(new Vector3(0, Jumpforce, 0),ForceMode.Impulse);
        }
    }
}
