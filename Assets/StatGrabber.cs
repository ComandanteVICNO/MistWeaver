using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatGrabber : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetFloat("Timer", 0);
        PlayerPrefs.SetInt("ConeCount", 0);
    }

    
}
