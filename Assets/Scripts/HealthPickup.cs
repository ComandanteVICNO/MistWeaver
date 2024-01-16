using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    Transform healthPickupTransform;
    public float healthAmount;
    public PlayerHealth playerHealth;
    public GameObject healthPickup;

    public GameObject parent;

    public AudioSource audioSource;
    public AudioClip healthPickupSound;
   

    void Start()
    {
        healthPickupTransform = GetComponent<Transform>();
    }

    
    void Update()
    {
        transform.position = new Vector3(healthPickupTransform.position.x, healthPickupTransform.position.y, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        else
        {
            playerHealth = other.GetComponent<PlayerHealth>();
            playerHealth.HealthPickup(healthAmount);

            audioSource.PlayOneShot(healthPickupSound);

            StartCoroutine(StartDestroy());
        }
    }

    IEnumerator StartDestroy()
    {
        Destroy(healthPickup);

        yield return new WaitForSecondsRealtime(healthPickupSound.length);

        Destroy(parent);
    }

}
