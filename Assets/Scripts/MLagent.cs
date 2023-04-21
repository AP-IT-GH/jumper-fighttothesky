using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using System;
using Unity.MLAgents.Sensors;

public class MLagent : Agent
{
    public float force = 20f;
    public float minVelocityLimit = 0.005f;
    public GameObject Target1;
    public GameObject Target2;
    public float negativeReward = 2f;
    public float positiveReward = 1f;
    private Rigidbody rb;
    private Rigidbody trb1;
    private Rigidbody trb2;
    private Vector3 spawnpoint = Vector3.zero;

    public override void Initialize()
    {
        rb = this.GetComponent<Rigidbody>();
        trb1 = Target1.GetComponent<Rigidbody>();
        trb2 = Target2.GetComponent<Rigidbody>();
        spawnpoint = transform.position;
        rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        // Target1 en Agent position
        sensor.AddObservation(Target1.transform.localPosition);
        sensor.AddObservation(Target2.transform.localPosition);
        sensor.AddObservation(this.transform.localPosition);

        // Agent velocity
        sensor.AddObservation(rb.velocity.y);
        sensor.AddObservation(trb1.velocity.z);
        sensor.AddObservation(trb2.velocity.z);
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        if (actions.DiscreteActions[0] > 0)
        {
            Thrust();
        }
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
            AddReward(-negativeReward);
            EndEpisode();
        }

        if (collision.gameObject.CompareTag("points"))
        {
            SetReward(positiveReward);
        }
    }
}
