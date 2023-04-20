using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.VisualScripting;

public class MLagent : Agent
{
    public float force = 15f;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
        
    }

    private void FixedUpdate()
    {
        if(Input.GetKey(KeyCode.UpArrow) == true)
        {
            Thrust();
        }
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if(collision.gameObject.CompareTag("obstacle") == true)
    //    {
    //        Destroy(collision.gameObject);
    //    }
    //}

    private void Thrust()
    {
        rb.AddForce(Vector3.up * force, ForceMode.Acceleration);
    }
}
