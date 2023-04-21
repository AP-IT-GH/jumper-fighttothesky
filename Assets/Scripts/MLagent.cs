using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.VisualScripting;
using Unity.MLAgents.Actuators;
using System;
using static UnityEditor.Progress;
using static UnityEngine.GraphicsBuffer;
using Unity.MLAgents.Sensors;
using UnityEditor.UIElements;

public class MLagent : Agent
{
    public float force = 20f;
    public float minVelocityLimit = 0.005f;
    public GameObject Target;
    public float negativeReward = 5f;
    public float positiefReward = 1f;
    private Rigidbody rb;
    private Rigidbody trb;
    private Vector3 spawnpoint = Vector3.zero;
    //private bool died = false;

    //float timeRunning = 0.0f;



    //starting function

    /* private void Start()
     {
         rb = this.GetComponent<Rigidbody>();
         spawnpoint = transform.position;
         rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
     }*/
    public override void Initialize()
    {
        rb = this.GetComponent<Rigidbody>();
        trb = Target.GetComponent<Rigidbody>();
        spawnpoint = transform.position;
        rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        // Target en Agent position
        sensor.AddObservation(Target.transform.localPosition);
        sensor.AddObservation(this.transform.localPosition);

        // Agent velocity
        sensor.AddObservation(rb.velocity.y);
        sensor.AddObservation(trb.velocity.z);
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        if (actions.DiscreteActions[0] > 0)
        {
            Thrust();
        }

/*        if (Math.Abs(rb.velocity.y) < minVelocityLimit)
        {
            SetReward(0.5f);
        }*/
    }

    public override void OnEpisodeBegin()
    {
        transform.position = spawnpoint;
    }


    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var discreteActionsOut = actionsOut.DiscreteActions;
        discreteActionsOut[0] = Input.GetKey(KeyCode.UpArrow) ? 1 : 0;
    }
    /*    private void FixedUpdate()
        {
            if(Input.GetKey(KeyCode.UpArrow) == true)
            {
                Thrust();
            }
        }*/

/*    private void Update()
    {
    
       timeRunning += Time.deltaTime;
        if (!died)
        {
            SetReward(timeRunning * positiefReward);
            timeRunning = 0.0f; //Restart counting
        }
        
        
    }*/

    private void Thrust()
    {
        if (Math.Abs(rb.velocity.y) < minVelocityLimit)
        {
            rb.AddForce(new Vector3(0, force, 0), ForceMode.Impulse);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("obstacle"))
        {
            //died = true;
            AddReward(-negativeReward);
            EndEpisode();
        }

        if (collision.gameObject.CompareTag("points"))
        {
            SetReward(positiefReward);
        }
    }
}
