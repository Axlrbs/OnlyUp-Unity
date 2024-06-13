using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRespawn : MonoBehaviour
{
    public float threshold;
    public float x;
    public float y;
    public float z;

    private void FixedUpdate()
    {
        if (transform.position.y < threshold)
        {
            transform.position = new Vector3(x, y, z);
        }
    }
}
