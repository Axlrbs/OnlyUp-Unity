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

    public float xCheat;
    public float yCheat;
    public float zCheat;

    private void FixedUpdate()
    {
        if (transform.position.y < threshold)
        {
            transform.position = new Vector3(x, y, z);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            transform.position = new Vector3(xCheat, yCheat, zCheat);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            transform.position = new Vector3(x, y, z);
        }
    }
}
