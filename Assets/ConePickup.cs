using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConePickup : MonoBehaviour
{
    int coneCount;
    public GameObject parent;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        coneCount = PlayerPrefs.GetInt("ConeCount");
        int newConeCount = coneCount + 1;

        PlayerPrefs.SetInt("ConeCount", newConeCount);
        PlayerPrefs.Save();

        Object.Destroy(parent);
    }

}
