using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetConeCounter : MonoBehaviour
{
    private void Start()
    {
        PlayerPrefs.SetInt("ConeCount", 0);
        PlayerPrefs.Save();
    }
}
