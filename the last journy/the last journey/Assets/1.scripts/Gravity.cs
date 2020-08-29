using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    public float gravitational_constant = 9.8f;
    public float mass = 1;
    public Rigidbody body;

    void Update()
    {
        body.AddForce(0, 0, 100f);
    }
}
