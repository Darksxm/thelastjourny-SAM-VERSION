using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    public void FixedUpdate()
    {
        var p = transform.position;
        p.y -= 0.05f;
        transform.position = p;
    }
}
