using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyPickup : MonoBehaviour
{
    Transform energyPickupTransform;
    public PlayerAttack playerAttack;
    public GameObject energyPickup;

    public GameObject parent;

    public AudioSource audioSource;
    public AudioClip energyPickupSound;

    void Start()
    {
        energyPickupTransform = GetComponent<Transform>();
    }


    void Update()
    {
        transform.position = new Vector3(energyPickupTransform.position.x, energyPickupTransform.position.y, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        else
        {
            playerAttack = other.GetComponent<PlayerAttack>();
            playerAttack.EnergyPickup();

            audioSource.PlayOneShot(energyPickupSound);

            StartCoroutine(StartDestroy());
        }
    }

    IEnumerator StartDestroy()
    {
        Destroy(energyPickup);

        yield return new WaitForSecondsRealtime(energyPickupSound.length);

        Destroy(parent);
    }

}
