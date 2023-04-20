using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public float Movespeed = 3.5f;
    private Vector3 spawnpoint = Vector3.zero;


    private void Start()
    {
        spawnpoint = transform.position;
    }

    private void Update()
    {
        transform.Translate(Vector3.back * Movespeed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall") | collision.gameObject.CompareTag("mlagent"))
        {
            transform.position = spawnpoint;
        }
    }
}
