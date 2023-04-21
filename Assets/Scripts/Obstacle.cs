using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class Obstacle : MonoBehaviour
{
    public float Movespeed = 3.5f;
    private Vector3 spawnpoint = Vector3.zero;
    private quaternion spawnRotation = quaternion.EulerXYZ(0,0,0);
    private Rigidbody rb;


    private void Start()
    {
        spawnpoint = transform.position;
        spawnRotation = transform.rotation;
        rb = this.GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ;
    }

    private void Update()
    {
        transform.Translate(Vector3.back * Movespeed * Time.deltaTime);
        if (transform.localPosition.y < 0 | transform.localPosition.z < -8)
        {
            transform.position = spawnpoint;
            transform.localRotation = spawnRotation;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall") | collision.gameObject.CompareTag("mlagent"))
        {
            transform.position = spawnpoint;
            transform.localRotation = spawnRotation;
            Movespeed = Random.Range(1, 10);
        }
    }
}
