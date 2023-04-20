using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public float Movespeed = 3.5f;
    public Vector3 Spawnpoint = Vector3.zero;


    private void Start()
    {
        Spawnpoint = transform.position;
    }

    private void Update()
    {
        transform.Translate(Vector3.back * Movespeed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall") | collision.gameObject.CompareTag("mlagent") == true)
        {
            transform.position = Spawnpoint;
        }
    }
}
