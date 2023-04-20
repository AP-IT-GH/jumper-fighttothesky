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
    public float minVelocityLimit = 0.05f;
    public Transform Target;
    public float negativeReward = 5f;
    public float positiefReward = 1f;
    private Rigidbody rb;
    private Vector3 spawnpoint = Vector3.zero;
    private bool died = false;


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
        spawnpoint = transform.position;
        rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        // Target en Agent position
        sensor.AddObservation(Target.localPosition);
        sensor.AddObservation(this.transform.localPosition);

    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        print("onaction");
        if (actions.DiscreteActions[0] > 0)
        {
            Thrust();
        }
    }

    public override void OnEpisodeBegin()
    {
        transform.position = spawnpoint;
        print("onepisode");
    }


    public override void Heuristic(in ActionBuffers actionsOut)
    {
        print("jup");
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

    private void Update()
    {
        if (!died)
        {
            SetReward(Time.deltaTime*positiefReward);
        }
        
    }

    private void Thrust()
    {
        print("trust");
        if (Math.Abs(rb.velocity.y) < minVelocityLimit)
        {
            print("accusal trust");
            rb.AddForce(new Vector3(0, force, 0), ForceMode.Impulse);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("obstacle"))
        {
            print("died");
            died = true;
            AddReward(negativeReward);
            EndEpisode();
        }
    }
}
