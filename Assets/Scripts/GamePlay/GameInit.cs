using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInit : MonoBehaviour
{
    private void Awake()
    {
        if(!PlayerPrefs.HasKey("Sound")|| !PlayerPrefs.HasKey("Hints")|| !PlayerPrefs.HasKey("Tutorial"))
        {
            PlayerPrefs.SetInt("Sound", 0);
            PlayerPrefs.SetInt("Hints", 5);
            PlayerPrefs.SetInt("Tutorial", 0);
            PlayerPrefs.SetInt("Pause", 0);
            PlayerPrefs.SetInt("TillNextAD", 0);
        }
    }
}
