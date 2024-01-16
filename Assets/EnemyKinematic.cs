using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKinematic : MonoBehaviour
{
    public Rigidbody rb;

    private void Start()
    {
        rb.isKinematic = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            rb.isKinematic = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            rb.isKinematic = false;
        }
    }

    

}
