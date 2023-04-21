using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using System;
using Unity.MLAgents.Sensors;

public class MLagent : Agent
{
    public float force = 20f;
    public float minVelocityLimit = 0.005f;

    //Targets
    public GameObject Target1;
    public GameObject Target2;
    private Rigidbody trb1;
    private Rigidbody trb2;

    //Rewards
    public float negativeReward = 2f;
    public float positiveReward = 1f;


    private Rigidbody rb;
    private Vector3 spawnpoint = Vector3.zero;

    public override void Initialize()
    {
        // Get all rigidbodies of the objects you need.
        rb = this.GetComponent<Rigidbody>();
        trb1 = Target1.GetComponent<Rigidbody>();
        trb2 = Target2.GetComponent<Rigidbody>();
        spawnpoint = transform.position;

        // Only move in Y direction.
        rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        // Targets and Agent position
        sensor.AddObservation(Target1.transform.localPosition);
        sensor.AddObservation(Target2.transform.localPosition);
        sensor.AddObservation(this.transform.localPosition);

        // Targets and Agent velocity
        sensor.AddObservation(rb.velocity.y);
        sensor.AddObservation(trb1.velocity.z);
        sensor.AddObservation(trb2.velocity.z);
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        if (actions.DiscreteActions[0] > 0)
        {
            // Jump
            Thrust();
        }
    }

    public override void OnEpisodeBegin()
    {
        // Respawn cube in starting position.
        transform.position = spawnpoint;
    }


    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var discreteActionsOut = actionsOut.DiscreteActions;
        // Move with upkey
        discreteActionsOut[0] = Input.GetKey(KeyCode.UpArrow) ? 1 : 0;
    }

    private void Thrust()
    {
        // Only jump if not in the air.
        if (Math.Abs(rb.velocity.y) < minVelocityLimit)
        {
            rb.AddForce(new Vector3(0, force, 0), ForceMode.Impulse);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // If he has a collision with a obstacle he will get a negative reward.
        if (collision.gameObject.CompareTag("obstacle"))
        {
            AddReward(-negativeReward);
            EndEpisode();
        }
        // If he has a collision with the wall behind the obstacle he will get a positive reward.
        if (collision.gameObject.CompareTag("points"))
        {
            SetReward(positiveReward);
        }
    }
}
