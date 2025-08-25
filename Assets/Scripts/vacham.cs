using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vacham : MonoBehaviour
{
    private Collider mycollider;
    private Rigidbody rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        mycollider = GetComponent<Collider>();
    }   
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "enemy") {
            Destroy(gameObject);
        }
    }
}
