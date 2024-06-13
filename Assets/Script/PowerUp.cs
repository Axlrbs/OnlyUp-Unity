using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    
    private void OnTriggerEnter(gameObject other) 
    {
        if (other.name == "Player") 
        {
            Player.MovementSpeed += 5;
            Destroy(gameObject);
        }
    }
}
